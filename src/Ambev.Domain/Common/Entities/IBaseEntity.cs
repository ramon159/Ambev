namespace Ambev.Domain.Common.Entities
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        string? CreatedBy { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
        string? UpdatedBy { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
        string? DeletedBy { get; set; }
        bool IsDeleted { get; set; }
        IReadOnlyCollection<BaseEvent> DomainEvents { get; }

        void AddDomainEvent(BaseEvent domainEvent);
        void ClearDomainEvents();
        void RemoveDomainEvent(BaseEvent domainEvent);
    }
}