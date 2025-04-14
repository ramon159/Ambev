using MediatR;

namespace Ambev.Application.Features.Products.Queries.GetProductCategories
{
    public class GetAllCategoriesQuery : IRequest<List<string>>
    {
    }
}