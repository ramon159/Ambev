using Ambev.Domain.Features.Sales.Queries.GetSale;
using Ambev.Shared.Common.Http;
using MediatR;

namespace Ambev.Domain.Features.Sales.Queries.GetSaleWithPagination
{
    public class GetAllSalesCommand : QueryParameters, IRequest<PaginedList<GetSaleResponse>>
    {
    }
}