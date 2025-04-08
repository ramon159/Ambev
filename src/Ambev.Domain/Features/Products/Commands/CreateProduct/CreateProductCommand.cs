using Ambev.Shared.Dtos;
using Ambev.Shared.Entities;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<CreateProductResponse>
    {
        public string Title { get; init; } = string.Empty;
        public double Price { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public required RatingDto Rating { get; init; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateProductCommand, Product>().ReverseMap();
            }
        }
    }
}
