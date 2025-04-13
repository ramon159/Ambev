using Ambev.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.Application.Contracts.Dtos.Sales
{
    public class SaleItemDto
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<SaleItemDto, SaleItem>().ReverseMap();
            }
        }
    }
}
