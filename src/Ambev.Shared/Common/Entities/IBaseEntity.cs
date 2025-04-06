
namespace Ambev.Shared.Common.Entities
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTimeOffset Created { get; set; }
        string? CreatedBy { get; set; }
        DateTimeOffset? Updated { get; set; }
        string? UpdatedBy { get; set; }
        DateTimeOffset? Deleted { get; set; }
        string? DeletedBy { get; set; }
        bool IsDeleted { get; set; }
    }
}