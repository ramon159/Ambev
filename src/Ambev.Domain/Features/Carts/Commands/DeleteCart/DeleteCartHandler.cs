using Ambev.Shared.Entities.Sales.Carts;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Features.Carts.Commands.DeleteCart
{
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, DeleteCartResponse>
    {
        private readonly IRepositoryBase<Cart> _cartRepository;
        public DeleteCartHandler(IRepositoryBase<Cart> cartRepository)
        {
            _cartRepository=cartRepository;
        }

        public async Task<DeleteCartResponse> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var entity = await _cartRepository.GetByIdAsync(
                request.Id,
                includes: c => c.Include(c => c.Products),
                cancellationToken: cancellationToken
                );

            Guard.Against.NotFound(request.Id, entity);

            await _cartRepository.DeleteAsync(entity);

            return new DeleteCartResponse { Success = true };
        }
    }
}
