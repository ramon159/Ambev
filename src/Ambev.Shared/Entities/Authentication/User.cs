using Ambev.Shared.Common.Entities;
using Ambev.Shared.Enums;
using Ambev.Shared.ValueObjects;

namespace Ambev.Shared.Entities.Authentication
{
    public class User : BaseEntity
    {
        public required string Email { get; set; } = string.Empty;
        public required string UserName { get; set; } = string.Empty;
        public required string PasswordHash { get; set; } = string.Empty;
        public Name? Name { get; set; }
        public Address? Address { get; set; }
        public string? Phone { get; set; } = string.Empty;
        public required UserStatus Status { get; set; } = UserStatus.None;
        public required UserRole Role { get; set; } = UserRole.None;
    }
}
