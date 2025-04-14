using Ambev.Application.Features.Sales.Queries.GetSale;
using Ambev.Application.Models.Http;
using MediatR;

namespace Ambev.Application.Features.Sales.Queries.GetSaleWithPagination
{
    public class GetAllSalesQuery : QueryParameters, IRequest<PaginedList<GetSaleResponse>>
    {
    }
}