using Ambev.Domain.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ambev.Domain.Entities.Sales.Carts
{
    public class Cart : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
    }
}
