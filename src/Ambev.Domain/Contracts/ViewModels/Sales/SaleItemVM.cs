namespace Ambev.Domain.Contracts.ViewModels.Sales
{
    public class SaleItemVM
    {
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; } = 0;
        public Guid ProductId { get; set; }
    }
}