using MediatR;

namespace Ambev.Domain.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<DeleteProductResponse>
    {
        public Guid Id { get; set; }
    }
}