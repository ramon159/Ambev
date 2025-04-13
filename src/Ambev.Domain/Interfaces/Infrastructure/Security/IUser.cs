namespace Ambev.Domain.Interfaces.Infrastructure.Security
{
    public interface IUser
    {
        string? Id { get; }
        string? UserName { get; }
        string? Role { get; }
    }
}
