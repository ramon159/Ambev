using MediatR;

namespace Ambev.Domain.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public Guid Id { get; set; }
    }
}