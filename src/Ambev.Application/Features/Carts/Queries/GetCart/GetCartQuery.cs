using MediatR;

namespace Ambev.Application.Features.Carts.Queries.GetCart
{
    public class GetCartQuery : IRequest<GetCartResponse>
    {
        public Guid Id { get; set; }
    }
}
