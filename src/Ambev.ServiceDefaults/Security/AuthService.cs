using Ambev.Domain.Entities.Authentication;
using Ambev.Shared.Enums;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ambev.Shared.Interfaces.Infrastructure.Security;

namespace Ambev.ServiceDefaults.Security
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryBase<User> _userRepository;

        public AuthService(IRepositoryBase<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));

            if (user == null) return false;

            return string.Equals(user.Role.ToString(), role, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policy)
        {
            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));

            if (user == null) return false;

            return policy switch
            {
                "Admin" => user.Role == UserRole.Admin,
                _ => false
            };
        }
    }
}
