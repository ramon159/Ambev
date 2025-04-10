namespace Ambev.Shared.Interfaces.Infrastructure.Security
{
    public interface ITokenService
    {
        string GenerateToken(IUser user);
    }
}