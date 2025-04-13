using Ambev.Domain.Common.Entities;

namespace Ambev.Domain.Entities.Sales.Products
{
    public class Rating : BaseEntity
    {
        public double Rate { get; set; }
        public int Count { get; set; }
    }
}
