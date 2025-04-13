using MediatR;

namespace Ambev.Application.Features.Carts.Commands.DeleteCart
{
    public class DeleteCartCommand : IRequest<DeleteCartResponse>
    {
        public Guid Id { get; set; }
    }
}
