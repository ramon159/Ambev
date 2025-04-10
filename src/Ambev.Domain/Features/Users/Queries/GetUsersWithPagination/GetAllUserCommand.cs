using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using Ambev.Domain.Features.Users.Queries.GetUser;
using Ambev.Shared.Common.Http;
using MediatR;

namespace Ambev.Domain.Features.Users.Queries.GetUsersWithPagination
{
    [Authorize(Roles = Roles.Customer)]
    public class GetAllUserCommand : QueryParameters, IRequest<PaginedList<GetUserResponse>>
    {
    }
}
