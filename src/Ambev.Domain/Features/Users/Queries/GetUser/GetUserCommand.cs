using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using MediatR;

namespace Ambev.Domain.Features.Users.Queries.GetUser
{
    [Authorize(Roles = Roles.Admin)]
    public class GetUserCommand : IRequest<GetUserResponse>
    {
        public Guid Id { get; set; }
    }
}
