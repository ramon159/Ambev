using Ambev.Domain.Entities.Sales.Products;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Application.Features.Products.Queries.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductQuery, GetProductResponse>
    {
        private readonly IRepositoryBase<Product> _productRepository;
        private readonly IMapper _mapper;

        public GetProductHandler(IRepositoryBase<Product> productRepository, IMapper mapper)
        {
            _productRepository=productRepository;
            _mapper=mapper;
        }

        public async Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetByIdAsync(request.Id, includes: p => p.Include(p => p.Rating), cancellationToken: cancellationToken);
            Guard.Against.NotFound(request.Id, entity);
            return _mapper.Map<GetProductResponse>(entity);
        }
    }
}
