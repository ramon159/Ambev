using MediatR;

namespace Ambev.Domain.Features.Carts.Queries.GetCart
{
    public class GetCartCommand : IRequest<GetCartResponse>
    {
        public Guid Id { get; set; }
    }
}
