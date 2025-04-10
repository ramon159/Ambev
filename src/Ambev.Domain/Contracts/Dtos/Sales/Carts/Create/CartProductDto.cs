using Ambev.Shared.Entities.Sales;
using AutoMapper;

namespace Ambev.Domain.Contracts.Dtos.Sales.Carts.Create
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
