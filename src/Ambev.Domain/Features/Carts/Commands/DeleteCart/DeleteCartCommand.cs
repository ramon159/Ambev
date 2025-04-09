using MediatR;

namespace Ambev.Domain.Features.Carts.Commands.DeleteCart
{
    public class DeleteCartCommand : IRequest<DeleteCartResponse>
    {
        public Guid Id { get; set; }
    }
}
