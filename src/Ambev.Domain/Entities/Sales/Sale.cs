using Ambev.Domain.Common.Entities;
using Ambev.Domain.Entities.Authentication;
using Ambev.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.Domain.Entities.Sales
{
    public class Sale : BaseEntity
    {
        public Sale()
        {
            SaleNumber = GenerateSaleNumber();
        }

        public string SaleNumber { get; private set; }
        public Guid? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public User? User { get; set; }
        public string Branch { set; get; } = string.Empty;
        public SaleStatus Status { get; set; } = SaleStatus.PendingPayment;
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
        public string GenerateSaleNumber()
        {
            var guid = Guid.NewGuid();
            var bytes = guid.ToByteArray();

            var part1 = BitConverter.ToUInt32(bytes, 0) % 10_000_000;
            var part2 = BitConverter.ToUInt32(bytes, 4) % 10_000_000;

            return $"{part1:D7}-{part2:D7}";
        }
    }
}
