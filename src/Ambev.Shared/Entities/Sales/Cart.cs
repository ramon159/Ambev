using Ambev.Shared.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ambev.Shared.Entities.Sales
{
    public class Cart : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
    }
}
