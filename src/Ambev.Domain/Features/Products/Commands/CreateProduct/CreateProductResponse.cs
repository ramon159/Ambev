using Ambev.Domain.Contracts.Dtos.Sales.Products;
using Ambev.Shared.Entities.Sales.Products;
using AutoMapper;

namespace Ambev.Domain.Features.Products.Commands.CreateProduct
{
    public record CreateProductResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public RatingDto Rating { get; init; } = new();
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateProductResponse, Product>().ReverseMap();
            }
        }
    }
}
