using Ambev.Shared.Entities;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Domain.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
    {
        private readonly IRepositoryBase<Product> _productRepository;

        public DeleteProductHandler(IRepositoryBase<Product> productRepository)
        {
            _productRepository=productRepository;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetByIdAsync(
                request.Id,
                includes: p => p.Include(p => p.Rating)
                );

            Guard.Against.NotFound(request.Id, entity);

            await _productRepository.DeleteAsync(entity);

            return new DeleteProductResponse { Success = true };
        }
    }
}
