using Ambev.Domain.Dtos;
using Ambev.Shared.Entities;
using AutoMapper;

namespace Ambev.Domain.Features.Products.Queries.GetProduct
{
    public class GetProductResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public required RatingDto Rating { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GetProductResponse, Product>().ReverseMap();
            }
        }
    }
}
