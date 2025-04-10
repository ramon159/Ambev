using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using MediatR;

namespace Ambev.Domain.Features.Users.Commands.DeleteUser
{
    [Authorize(Roles = Roles.Customer)]
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public Guid Id { get; set; }
    }
}