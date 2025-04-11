using Ambev.Domain.Contracts.ViewModels.Common;
using Ambev.Domain.Entities.Sales;

namespace Ambev.Domain.Contracts.ViewModels.Sales
{
    public class SaleVM : BaseViewModel
    {
        public int SaleNumber { get; set; }
        public Guid? CustomerId { get; set; }
        public string Branch { set; get; } = string.Empty;
        public SaleStatus Status { get; set; } = SaleStatus.PendingPayment;
        public decimal TotalAmount { get; set; } = 0;
        public List<SaleItemVM> Items { get; set; } = [];
        public DateTimeOffset CreatedAt { get; set; }
    }
}