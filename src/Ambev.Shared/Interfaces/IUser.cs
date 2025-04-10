namespace Ambev.Shared.Interfaces
{
    public interface IUser
    {
        string? Id { get; }
        string? UserName { get; }
        string? Role { get; }
    }
}
