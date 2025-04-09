using MediatR;

namespace Ambev.Domain.Features.Products.Queries.GetProductCategories
{
    public class GetAllCategoriesCommand : IRequest<List<string>>
    {
    }
}