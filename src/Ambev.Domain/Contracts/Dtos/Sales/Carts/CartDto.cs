using Ambev.Shared.Entities.Sales.Carts;
using AutoMapper;

namespace Ambev.Domain.Contracts.Dtos.Sales.Carts
{
    public class CartDto
    {
        public List<CartProductDto> Products { get; set; } = new List<CartProductDto>();
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CartDto, Cart>().ReverseMap();
            }
        }
    }
}