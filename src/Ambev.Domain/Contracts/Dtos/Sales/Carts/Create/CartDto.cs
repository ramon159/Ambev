using Ambev.Shared.Entities.Sales;
using AutoMapper;

namespace Ambev.Domain.Contracts.Dtos.Sales.Carts.Create
{
    public class CartDto
    {
        public Guid UserId { get; set; }
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
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