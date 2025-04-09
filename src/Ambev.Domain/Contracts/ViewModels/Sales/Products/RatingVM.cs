using Ambev.Shared.Entities.Sales;
using AutoMapper;

namespace Ambev.Domain.Contracts.ViewModels.Sales.Products
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