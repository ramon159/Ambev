using Ambev.Domain.Entities.Sales.Carts;
using AutoMapper;

namespace Ambev.Application.Contracts.Dtos.Sales.Carts
{
    public class CartProductDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 0;
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CartProductDto, CartProduct>().ReverseMap();
            }
        }
    }

}
