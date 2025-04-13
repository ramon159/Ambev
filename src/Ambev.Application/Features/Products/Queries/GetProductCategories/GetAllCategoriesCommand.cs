using MediatR;

namespace Ambev.Application.Features.Products.Queries.GetProductCategories
{
    public class GetAllCategoriesCommand : IRequest<List<string>>
    {
    }
}