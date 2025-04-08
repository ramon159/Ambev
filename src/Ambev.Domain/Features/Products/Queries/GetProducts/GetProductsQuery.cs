using Ambev.Shared.Common.Http;
using Ambev.Shared.Dtos;
using Ambev.Shared.Entities;
using MediatR;

namespace Ambev.Domain.Features.Products.Queries.GetProducts
{
    public class GetProductsQuery : QueryParameters, IRequest<PaginedList<Product>>
    {
    }
}
