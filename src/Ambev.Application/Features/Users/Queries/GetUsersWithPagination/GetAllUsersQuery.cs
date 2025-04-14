using Ambev.Application.Features.Users.Queries.GetUser;
using Ambev.Application.Models.Http;
using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using MediatR;

namespace Ambev.Application.Features.Users.Queries.GetUsersWithPagination
{
    [Authorize(Roles = Roles.Admin)]
    public class GetAllUsersQuery : QueryParameters, IRequest<PaginedList<GetUserResponse>>
    {
    }
}
