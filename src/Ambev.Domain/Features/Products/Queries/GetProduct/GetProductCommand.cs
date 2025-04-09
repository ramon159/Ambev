using MediatR;

namespace Ambev.Domain.Features.Products.Queries.GetProduct
{
    public class GetProductCommand : IRequest<GetProductResponse>
    {
        public Guid Id { get; set; }
    }
}
