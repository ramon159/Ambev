﻿using Ambev.Domain.Entities.Sales.Products;
using AutoMapper;

namespace Ambev.Application.Contracts.Dtos.Sales.Products
{
    public class ProductDto
    {
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public RatingDto Rating { get; set; } = new();
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Product, ProductDto>().ReverseMap();
            }
        }
    }
}
