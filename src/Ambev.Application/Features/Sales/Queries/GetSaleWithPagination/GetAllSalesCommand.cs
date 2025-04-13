using Ambev.Application.Features.Sales.Queries.GetSale;
using Ambev.Application.Models.Http;
using MediatR;

namespace Ambev.Application.Features.Sales.Queries.GetSaleWithPagination
{
    public class GetAllSalesCommand : QueryParameters, IRequest<PaginedList<GetSaleResponse>>
    {
    }
}