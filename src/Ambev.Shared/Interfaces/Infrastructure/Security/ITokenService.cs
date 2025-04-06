namespace Ambev.Shared.Interfaces.Infrastructure.Security
{
    public interface ITokenService
    {
        string GenerateToken(IAuthenticationUser user);
    }
}