using Ambev.Shared.Entities;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Product> _productRepository;

        public UpdateProductHandler(IMapper mapper, IRepositoryBase<Product> productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(
                request.Id, 
                includes: p => p.Include(p => p.Rating)
                );

            Guard.Against.NotFound(request.Id, product);

            product.Title = request.Title;
            product.Price = request.Price;
            product.Description = request.Description;
            product.Category = request.Category;
            product.Image = request.Image;

            if (product.Rating != null)
            {
                product.Rating.Rate = request.Rating.Rate;
                product.Rating.Count = request.Rating.Count;
            }

            var result = await _productRepository.UpdateAsync(product, cancellationToken);
            return _mapper.Map<UpdateProductResponse>(result);
        }
    }
}
