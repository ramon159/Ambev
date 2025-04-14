using Ambev.Application.Features.Carts.Queries.GetCart;
using Ambev.Application.Models.Http;
using MediatR;

namespace Ambev.Application.Features.Carts.Queries.GetCartWithPagination
{
    public class GetAllCartsQuery : QueryParameters, IRequest<PaginedList<GetCartResponse>>
    {
    }
}