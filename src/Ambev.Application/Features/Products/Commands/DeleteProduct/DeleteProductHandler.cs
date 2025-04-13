using Ambev.Domain.Entities.Sales.Products;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
    {
        private readonly IRepositoryBase<Product> _productRepository;

        public DeleteProductHandler(IRepositoryBase<Product> productRepository)
        {
            _productRepository=productRepository;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetByIdAsync(
                request.Id,
                includes: p => p.Include(p => p.Rating)
                );

            Guard.Against.NotFound(request.Id, entity);

            await _productRepository.DeleteAsync(entity);

            return new DeleteProductResponse { Success = true };
        }
    }
}
