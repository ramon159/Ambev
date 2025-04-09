
namespace Ambev.Domain.Features.Carts.Commands.UpdateCart
{
    public interface IUpdateCommand
    {
        Guid Id { get; }

        void SetId(Guid id);
    }
}