using Ambev.Shared.Common.Entities;
using Ambev.Shared.Enums;
using Ambev.Shared.Interfaces;
using Ambev.Shared.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.Shared.Entities.Authentication
{
    public class User : BaseEntity, IUser
    {
        public required string Email { get; set; } = string.Empty;
        public required string UserName { get; set; } = string.Empty;
        public required string PasswordHash { get; set; } = string.Empty;
        public Name? Name { get; set; }
        public Address? Address { get; set; }
        public string? Phone { get; set; } = string.Empty;
        public UserStatus? Status { get; set; } = UserStatus.None;
        public UserRole? Role { get; set; } = UserRole.None;

        string? IUser.Id => this.Id.ToString();
        string? IUser.Role => this.Role.ToString();
    }
}
