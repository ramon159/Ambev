using Ambev.Domain.Features.Products.Queries.GetProduct;
using Ambev.Shared.Common.Http;
using MediatR;

namespace Ambev.Domain.Features.Products.Queries.GetProductWithPagination
{
    public class GetAllProductsCommand : QueryParameters, IRequest<PaginedList<GetProductResponse>>
    {
    }
}
