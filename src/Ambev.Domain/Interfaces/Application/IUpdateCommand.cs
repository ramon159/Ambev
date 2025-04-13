namespace Ambev.Domain.Interfaces.Application
{
    public interface IUpdateCommand
    {
        Guid Id { get; }

        void SetId(Guid id);
    }
}