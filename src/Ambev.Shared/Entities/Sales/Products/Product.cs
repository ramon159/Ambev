using Ambev.Shared.Common.Entities;

namespace Ambev.Shared.Entities.Sales.Products
{
    public class Product : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public Guid? RatingId { get; set; }
        public virtual Rating? Rating { get; set; }
    }
}
