namespace Ambev.Domain.Interfaces.Domain
{
    public interface IUpdateCommand
    {
        Guid Id { get; }

        void SetId(Guid id);
    }
}