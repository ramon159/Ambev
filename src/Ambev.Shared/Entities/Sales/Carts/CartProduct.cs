using Ambev.Shared.Common.Entities;
using Ambev.Shared.Entities.Sales.Products;

namespace Ambev.Shared.Entities.Sales.Carts
{
    public class CartProduct : BaseEntity
    {
        public Guid? CartId { get; set; }
        public Cart? Cart { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; } = 0;
        public Product? Product { get; set; }
        public bool MatchesProduct(CartProduct cartProduct)
        {
            if (cartProduct == null) return false;

            return ProductId == cartProduct?.ProductId;
        }

    }
}