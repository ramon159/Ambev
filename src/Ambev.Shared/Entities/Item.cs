using Ambev.Shared.Common.Entities;

namespace Ambev.Shared.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
    }
}
