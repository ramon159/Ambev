using Ambev.Domain.Entities.Sales;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Features.Sales.Commands.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Sale> _saleRepository;
        private readonly IRepositoryBase<SaleItem> _saleItemRepository;

        public UpdateSaleHandler(IMapper mapper, IRepositoryBase<Sale> saleRepository, IRepositoryBase<SaleItem> saleItemRepository)
        {
            _mapper = mapper;
            _saleRepository=saleRepository;
            _saleItemRepository=saleItemRepository;
        }

        public async Task<UpdateSaleResponse> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(
                 request.Id,
                includes: c => c.Include(c => c.Items)
                );

            Guard.Against.NotFound(request.Id, sale);

            await SynchronizeSaleItems(request, sale);

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
    }
}
