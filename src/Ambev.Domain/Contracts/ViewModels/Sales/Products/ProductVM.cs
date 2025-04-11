using Ambev.Domain.Contracts.ViewModels.Common;
using Ambev.Domain.Entities.Sales.Products;
using AutoMapper;

namespace Ambev.Domain.Contracts.ViewModels.Sales.Products
{
    public class ProductVM : BaseViewModel
    {
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public RatingVM? Rating { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<ProductVM, Product>().ReverseMap();
            }
        }
    }
}
