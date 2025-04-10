using Ambev.Domain.Features.Users.Queries.GetUser;
using Ambev.Shared.Common.Http;
using MediatR;

namespace Ambev.Domain.Features.Users.Queries.GetUsersWithPagination
{
    public class GetAllUserCommand : QueryParameters, IRequest<PaginedList<GetUserResponse>>
    {
    }
}
