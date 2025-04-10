using Ambev.Shared.Common.Entities;
using Ambev.Shared.Entities.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.Shared.Entities.Sales
{
    public class Sale : BaseEntity
    {
        public int SaleNumber { get; set; }
        public Guid? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public User? User { get; set; }
        public string Branch { set; get; } = string.Empty;
        public SaleStatus Status { get; set; } = SaleStatus.PendingPayment;
        public decimal TotalAmount { get; set; } = 0;
        public List<SaleItem> Items { get; set; } = [];
        public void CalculateTotalAmount()
        {
            TotalAmount = Items.Sum(i => i.Quantity * i.ProductPrice);

        }
    }
}
