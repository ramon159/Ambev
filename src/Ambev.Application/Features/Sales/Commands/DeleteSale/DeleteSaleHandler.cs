using Ambev.Domain.Entities.Sales;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Application.Features.Sales.Commands.DeleteSale
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
    {
        private readonly IRepositoryBase<Sale> _saleRepository;
        public DeleteSaleHandler(IRepositoryBase<Sale> saleRepository)
        {
            _saleRepository=saleRepository;
        }

        public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _saleRepository.GetByIdAsync(
                request.Id,
                includes: c => c.Include(c => c.Items),
                cancellationToken: cancellationToken
                );

            Guard.Against.NotFound(request.Id, entity);

            await _saleRepository.DeleteAsync(entity);

            return new DeleteSaleResponse { Success = true };
        }
    }
}
