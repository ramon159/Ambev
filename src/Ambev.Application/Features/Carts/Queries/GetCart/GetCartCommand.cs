using MediatR;

namespace Ambev.Application.Features.Carts.Queries.GetCart
{
    public class GetCartCommand : IRequest<GetCartResponse>
    {
        public Guid Id { get; set; }
    }
}
