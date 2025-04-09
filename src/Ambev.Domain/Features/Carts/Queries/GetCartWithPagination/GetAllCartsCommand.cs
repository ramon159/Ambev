using Ambev.Domain.Features.Carts.Queries.GetCart;
using Ambev.Shared.Common.Http;
using MediatR;

namespace Ambev.Domain.Features.Carts.Queries.GetCartWithPagination
{
    public class GetAllCartsCommand : QueryParameters, IRequest<PaginedList<GetCartResponse>>
    {
    }
}