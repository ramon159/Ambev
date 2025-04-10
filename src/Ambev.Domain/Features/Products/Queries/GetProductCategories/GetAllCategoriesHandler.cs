using Ambev.Shared.Entities.Sales.Products;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Features.Products.Queries.GetProductCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesCommand, List<string>>
    {
        private readonly IRepositoryBase<Product> _productRepository;

        public GetAllCategoriesHandler(IRepositoryBase<Product> productRepository)
        {
            this._productRepository=productRepository;
        }

        public async Task<List<string>> Handle(GetAllCategoriesCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.DbSet.Select(p => p.Category).Distinct().ToListAsync(cancellationToken);
        }
    }
}
