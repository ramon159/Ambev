using Ambev.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.Domain.Contracts.Dtos.Sales
{
    public class SaleDto
    {
        public List<SaleItemDto> Items { get; set; } = [];
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<SaleDto, Sale>().ReverseMap();
            }
        }
    }
}