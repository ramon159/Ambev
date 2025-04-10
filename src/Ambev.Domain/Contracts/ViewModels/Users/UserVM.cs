using Ambev.Domain.Contracts.ViewModels.Common;
using Ambev.Shared.Entities.Authentication;
using Ambev.Shared.Enums;
using Ambev.Shared.ValueObjects;
using AutoMapper;

namespace Ambev.Domain.Contracts.ViewModels.Users
{
    public class UserVM : BaseViewModel
    {
        public required string Email { get; set; } = string.Empty;
        public required string UserName { get; set; } = string.Empty;
        public required Name Name { get; set; }
        public required Address Address { get; set; }
        public string? Phone { get; set; } = string.Empty;
        public required UserStatus Status { get; set; } = UserStatus.None;
        public required UserRole Role { get; set; } = UserRole.None;
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UserVM, User>().ReverseMap();
            }
        }
    }
}