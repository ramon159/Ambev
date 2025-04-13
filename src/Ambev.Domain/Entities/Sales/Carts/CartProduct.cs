using Ambev.Domain.Common.Entities;
using Ambev.Domain.Entities.Sales.Products;

namespace Ambev.Domain.Entities.Sales.Carts
{
    public class CartProduct : BaseEntity
    {
        public Guid? CartId { get; set; }
        public Cart? Cart { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public Product? Product { get; set; }
        public bool MatchesProduct(CartProduct cartProduct)
        {
            if (cartProduct == null) return false;

            return ProductId == cartProduct?.ProductId;
        }

    }
}