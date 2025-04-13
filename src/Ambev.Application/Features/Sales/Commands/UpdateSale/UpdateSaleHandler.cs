using Ambev.Domain.Entities.Sales;
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

            await SynchronizeSaleItems(request, sale);
            await ValidateAndBindSaleItems(sale, cancellationToken);
            sale.CalculateTotalAmount();

            await _saleRepository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UpdateSaleResponse>(sale);

        }


        private async Task SynchronizeSaleItems(UpdateSaleCommand request, Sale sale)
        {
            var incomingSaleItems = request.Items.Select(p =>
            {
                return new SaleItem
                {
                    SaleId = sale.Id,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                };
            });

            foreach (var saleItem in sale.Items)
            {
                var existingSaleItem = incomingSaleItems.FirstOrDefault(dto => dto.MatchesProduct(saleItem));

                if (existingSaleItem != null)
                {
                    saleItem.Quantity = existingSaleItem.Quantity;
                    continue;
                }
                _saleItemRepository.DbSet.Remove(saleItem);
            }

            var productsToAdd = incomingSaleItems
                .Where(dto => !sale.Items.Any(cp => cp.MatchesProduct(dto)))
                .ToList();

            await _saleItemRepository.DbSet.AddRangeAsync(productsToAdd);
        }
        private async Task ValidateAndBindSaleItems(Sale sale, CancellationToken cancellationToken)
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
                item.SetProduct(product);
            }
        }
    }
}
