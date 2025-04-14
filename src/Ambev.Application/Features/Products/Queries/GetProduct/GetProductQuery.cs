using MediatR;

namespace Ambev.Application.Features.Products.Queries.GetProduct
{
    public class GetProductQuery : IRequest<GetProductResponse>
    {
        public Guid Id { get; set; }
    }
}
