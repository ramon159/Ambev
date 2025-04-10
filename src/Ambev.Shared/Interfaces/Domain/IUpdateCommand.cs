namespace Ambev.Shared.Interfaces.Domain
{
    public interface IUpdateCommand
    {
        Guid Id { get; }

        void SetId(Guid id);
    }
}