using Ambev.Domain.Entities.Sales.Products;
using AutoMapper;

namespace Ambev.Application.Contracts.ViewModels.Sales.Products
{
    public class RatingVM
    {
        public double Rate { get; set; }
        public int Count { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<RatingVM, Rating>().ReverseMap();
            }
        }
    }
}