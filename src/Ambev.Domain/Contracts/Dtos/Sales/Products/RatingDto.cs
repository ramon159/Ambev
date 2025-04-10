using Ambev.Shared.Entities.Sales.Products;
using AutoMapper;

namespace Ambev.Domain.Contracts.Dtos.Sales.Products
{
    public class RatingDto
    {
        public double Rate { get; set; }
        public int Count { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<RatingDto, Rating>().ReverseMap();
            }
        }
    }
}
