using Ambev.Domain.Entities.Sales.Products;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Application.Features.Products.Queries.GetProductCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<string>>
    {
        private readonly IRepositoryBase<Product> _productRepository;

        public GetAllCategoriesHandler(IRepositoryBase<Product> productRepository)
        {
            _productRepository=productRepository;
        }

        public async Task<List<string>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.DbSet.Select(p => p.Category).Distinct().ToListAsync(cancellationToken);
        }
    }
}
