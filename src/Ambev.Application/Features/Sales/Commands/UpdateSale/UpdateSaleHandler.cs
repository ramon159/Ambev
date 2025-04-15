using Ambev.Domain.Entities.Sales;
using Ambev.Domain.Entities.Sales.Carts;
using Ambev.Domain.Entities.Sales.Products;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Application.Features.Sales.Commands.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Sale> _saleRepository;
        private readonly IRepositoryBase<SaleItem> _saleItemRepository;
        private readonly IRepositoryBase<Product> _productRepository;

        public UpdateSaleHandler(
            IMapper mapper,
            IRepositoryBase<Sale> saleRepository,
            IRepositoryBase<SaleItem> saleItemRepository,
            IRepositoryBase<Product> productRepository)
        {
            _mapper = mapper;
            _saleRepository=saleRepository;
            _saleItemRepository=saleItemRepository;
            _productRepository=productRepository;
        }

        public async Task<UpdateSaleResponse> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(
                 request.Id,
                includes: c => c.Include(c => c.Items)
                );

            Guard.Against.NotFound(request.Id, sale);

            if (sale.Status != SaleStatus.PendingPayment)
            {
                throw new InvalidOperationException("Sales can only be modified while in 'Pending Payment' status");
            }

            var incomingSaleItems = request.Items.Select(p =>
            {
                return new SaleItem
                {
                    SaleId = sale.Id,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                };
            }).ToList();

            await ValidateSaleItems(sale, cancellationToken);
            SynchronizeSaleItems(sale, incomingSaleItems, cancellationToken);
            await BindProductToSaleItem(sale);
            sale.CalculateTotalAmount();
            await _saleRepository.SaveChangesAsync();

            return _mapper.Map<UpdateSaleResponse>(sale);

        }

        private async Task BindProductToSaleItem(Sale sale)
        {
            foreach (var item in sale.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                Guard.Against.Null(product, nameof(product), $"Product {item.ProductId} not found");
                item.SetProduct(product);
            }
        }

        private async Task ValidateSaleItems(Sale sale, CancellationToken cancellationToken)
        {
            var productIds = sale.Items.Select(i => i.ProductId).Distinct().ToList();

            var products = await _productRepository.DbSet
                .AsNoTracking()
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            foreach (var item in sale.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                Guard.Against.Null(product, nameof(product), $"Product {item.ProductId} not found");
                //item.SetProduct(product);
            }
        }

        private void SynchronizeSaleItems(Sale sale, List<SaleItem> incomingItems, CancellationToken cancellationToken)
        {
            var existingDict = sale.Items.ToDictionary(si => new { si.SaleId, si.ProductId });
            var incomingDict = incomingItems.ToDictionary(si => new { si.SaleId, si.ProductId });

            var toUpdate = incomingItems
                .Where(incoming => existingDict.ContainsKey(new { incoming.SaleId, incoming.ProductId }))
                .ToList();

            var toAdd = incomingItems
                .Where(incoming => !existingDict.ContainsKey(new { incoming.SaleId, incoming.ProductId }))
                .ToList();

            var toRemove = sale.Items
                .Where(existing => !incomingDict.ContainsKey(new { existing.SaleId, existing.ProductId }))
                .ToList();

            foreach (var incomingItem in toUpdate)
            {
                var existingItem = existingDict[new { incomingItem.SaleId, incomingItem.ProductId }];
                existingItem.Quantity = incomingItem.Quantity;
            }

            foreach (var item in toRemove)
            {
                sale.Items.Remove(item);
                _saleItemRepository.DbSet.Remove(item);
            }

            _saleItemRepository.DbSet.AddRangeAsync(toAdd);

        }
    }
}
