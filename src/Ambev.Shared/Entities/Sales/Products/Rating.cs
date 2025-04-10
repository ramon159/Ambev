using Ambev.Shared.Common.Entities;

namespace Ambev.Shared.Entities.Sales.Products
{
    public class Rating : BaseEntity
    {
        public double Rate { get; set; }
        public int Count { get; set; }
    }
}
