using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using MediatR;

namespace Ambev.Application.Features.Users.Commands.DeleteUser
{
    [Authorize(Roles = Roles.Admin)]
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public Guid Id { get; set; }
    }
}