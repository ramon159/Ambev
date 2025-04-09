using Ambev.Shared.Common.Entities;

namespace Ambev.Shared.Entities.Sales
{
    public class Rating : BaseEntity
    {
        public double Rate { get; set; }
        public int Count { get; set; }
    }
}
