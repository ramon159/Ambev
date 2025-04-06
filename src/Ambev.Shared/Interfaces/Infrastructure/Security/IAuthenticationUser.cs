namespace Ambev.Shared.Interfaces.Infrastructure.Security
{
    public interface IAuthenticationUser
    {
        public string Id { get; }
        public string UserName { get; }
        public string Role { get; }
    }
}