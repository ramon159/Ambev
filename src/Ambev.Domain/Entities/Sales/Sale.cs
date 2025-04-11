using Ambev.Domain.Entities.Authentication;
using Ambev.Domain.ValueObjects;
using Ambev.Shared.Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.Domain.Entities.Sales
{
    public class Sale : BaseEntity
    {
        public int SaleNumber { get; set; }
        public Guid? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public User? User { get; set; }
        public string Branch { set; get; } = string.Empty;
        public SaleStatus Status { get; set; } = SaleStatus.PendingPayment;

        [NotMapped]
        public string StatusName => Status.ToString();
        public decimal TotalAmount { get; private set; } = 0;
        public decimal TotalDiscount { get; private set; } = 0;
        public decimal SubTotal { get; private set; } = 0;
        public Address? ShippingAddress { get; set; }
        public List<SaleItem> Items { get; set; } = [];
        public void CalculateTotalAmount()
        {
            Items.ForEach(i => i.ApplyQuantityDiscount());
            SubTotal = Items.Sum(i => i.Quantity * i.ProductPrice);
            TotalDiscount = Items.Sum(i => i.Quantity * i.Discount);
            TotalAmount = SubTotal - TotalDiscount;
        }

    }
}
