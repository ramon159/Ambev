using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.Shared.Common.Entities
{
    public abstract class BaseEntity
    {
        [Column(Order = 0)]
        public Guid Id { get; set; }
    }
}
