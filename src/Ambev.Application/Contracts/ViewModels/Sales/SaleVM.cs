using Ambev.Application.Contracts.ViewModels.Common;
using Ambev.Domain.Entities.Sales;
using Ambev.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.Application.Contracts.ViewModels.Sales
{
    public class SaleVM : BaseViewModel
    {
        public string SaleNumber { get; set; } = string.Empty;
        public Guid? CustomerId { get; set; }
        public string Branch { set; get; } = string.Empty;
        public SaleStatus Status { get; set; } = SaleStatus.PendingPayment;
        public string StatusName => Status.ToString();
        public decimal TotalAmount { get; private set; } = 0;
        public decimal TotalDiscount { get; private set; } = 0;
        public decimal SubTotal { get; private set; } = 0;
        public Address? ShippingAddress { get; set; }

        public List<SaleItemVM> Items { get; set; } = [];
        public DateTimeOffset CreatedAt { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<SaleVM, Sale>().ReverseMap();
            }
        }


    }
}