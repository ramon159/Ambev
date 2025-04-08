using Ambev.Shared.Common.Http;
using Ambev.Shared.Entities;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Features.Products.Queries.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, PaginedList<Product>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Product> _productRepository;

        public GetProductsHandler(IMapper mapper, IRepositoryBase<Product> productRepository)
        {
            this._mapper=mapper;
            this._productRepository=productRepository;
        }

        public async Task<PaginedList<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var paginedResult = await _productRepository.GetAllAsync(
                page: request.Page,
                pageSize: request.Size,
                sortTerm: request.Order,
                filters: request.Filters,
                includes: p => p.Include(p => p.Rating),
                cancellationToken: cancellationToken
            );
            return paginedResult;
        }
    }
}
