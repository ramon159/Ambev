using Ambev.Shared.Common;
using Ambev.Shared.Dtos;
using Ambev.Shared.Entities;
using AutoMapper;

namespace Ambev.Domain.Features.Products.Commands.CreateProduct
{
    public class CreateProductResponse : BaseCommandResponse
    {
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
