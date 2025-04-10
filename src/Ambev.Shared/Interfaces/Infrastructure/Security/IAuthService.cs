namespace Ambev.Shared.Interfaces.Infrastructure.Security
{
    public interface IAuthService
    {
        Task<bool> AuthorizeAsync(string userId, string policy);
        Task<bool> IsInRoleAsync(string userId, string role);
    }
}