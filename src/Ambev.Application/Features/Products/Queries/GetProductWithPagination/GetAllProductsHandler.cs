using Ambev.Application.Features.Products.Queries.GetProduct;
using Ambev.Application.Models.Http;
using Ambev.Domain.Entities.Sales.Products;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Application.Features.Products.Queries.GetProductWithPagination
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, PaginedList<GetProductResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Product> _productRepository;

        public GetAllProductsHandler(IMapper mapper, IRepositoryBase<Product> productRepository)
        {
            _mapper=mapper;
            _productRepository=productRepository;
        }

        public async Task<PaginedList<GetProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var paginedResult = await _productRepository.GetAllAsync(
                page: request.Page,
                pageSize: request.Size,
                sortTerm: request.Order,
                filters: request.Filters,
                includes: p => p.Include(p => p.Rating),
                cancellationToken: cancellationToken
            );

            var items = _mapper.Map<List<GetProductResponse>>(paginedResult.Items);

            return new PaginedList<GetProductResponse>(items, paginedResult.Count, request.Page, request.Size);
        }
    }
}
