using Ambev.Domain.Entities.Sales.Products;
using Ambev.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.Domain.Contracts.ViewModels.Sales
{
    public class SaleItemVM
    {
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<SaleItemVM, SaleItem>().ReverseMap();
            }
        }
    }
}