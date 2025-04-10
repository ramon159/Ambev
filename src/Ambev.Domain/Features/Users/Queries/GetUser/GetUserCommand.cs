using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using Ambev.Shared.Enums;
using MediatR;

namespace Ambev.Domain.Features.Users.Queries.GetUser
{
    [Authorize(Roles = Roles.Customer)]
    public class GetUserCommand : IRequest<GetUserResponse>
    {
        public Guid Id { get; set; }
    }
}
