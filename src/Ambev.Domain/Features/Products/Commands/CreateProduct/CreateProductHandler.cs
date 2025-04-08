using Ambev.Shared.Entities;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Products.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Product> _productRepository;

        public CreateProductHandler(IMapper mapper, IRepositoryBase<Product> productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            var result = await _productRepository.AddAsync(product, cancellationToken);
            return _mapper.Map<CreateProductResponse>(result);
        }
    }
}
