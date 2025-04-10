using Ambev.Shared.Common.Entities;
using Ambev.Shared.Entities.Sales.Products;

namespace Ambev.Shared.Entities.Sales
{
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; set; }
        public Sale? Sale { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; } = 0;
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
    }
}