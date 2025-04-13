using MediatR;

namespace Ambev.Application.Features.Products.Queries.GetProduct
{
    public class GetProductCommand : IRequest<GetProductResponse>
    {
        public Guid Id { get; set; }
    }
}
