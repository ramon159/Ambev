namespace Ambev.Domain.Interfaces.Infrastructure.Security
{
    public interface ITokenService
    {
        string GenerateToken(IUser user);
    }
}