using Ambev.Domain.Common.Entities;
using Ambev.Domain.Entities.Sales.Products;

namespace Ambev.Domain.Entities.Sales
{
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public bool MatchesProduct(SaleItem item)
        {
            if (item == null) return false;

            return ProductId == item?.ProductId;
        }

        // 1. Discount Tiers:
        //- 4+ items: 10% discount
        //- 10-20 items: 20% discount
        public void ApplyQuantityDiscount()
        {
            Discount = Quantity switch
            {
                >= 10 => ProductPrice * 0.20m,
                >= 4 => ProductPrice * 0.10m,
                _ => 0m
            };
        }

        public void SetProduct(Product product)
        {
            this.ProductId = product.Id;
            this.ProductPrice = product.Price;
            this.ProductName = product.Title;
        }
    }
}