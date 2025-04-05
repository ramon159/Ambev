namespace Ambev.Shared.Common.Entities
{
    public abstract class AuditableBaseEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset Updated { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? Deleted { get; set; }
        public string? DeletedBy { get; set; }
    }
}