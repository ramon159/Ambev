using Ambev.Domain.Entities.Sales.Carts;
using AutoMapper;

namespace Ambev.Application.Contracts.ViewModels.Sales.Carts
{
    public class CartProductVM
    {
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CartProductVM, CartProduct>().ReverseMap();
            }
        }
    }
}