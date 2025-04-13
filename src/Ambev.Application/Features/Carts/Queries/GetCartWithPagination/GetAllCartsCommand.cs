using Ambev.Application.Features.Carts.Queries.GetCart;
using Ambev.Application.Models.Http;
using MediatR;

namespace Ambev.Application.Features.Carts.Queries.GetCartWithPagination
{
    public class GetAllCartsCommand : QueryParameters, IRequest<PaginedList<GetCartResponse>>
    {
    }
}