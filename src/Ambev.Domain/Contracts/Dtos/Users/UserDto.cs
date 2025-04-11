using Ambev.Domain.Entities.Authentication;
using Ambev.Domain.ValueObjects;
using Ambev.Shared.Enums;
using AutoMapper;

namespace Ambev.Domain.Contracts.Dtos.Users
{
    public class UserDto
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Name? Name { get; set; }
        public Address? Address { get; set; }
        public string? Phone { get; set; } = string.Empty;
        public UserStatus Status { get; set; } = UserStatus.Active;
        public UserRole Role { get; set; } = UserRole.Customer;

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UserDto, User>().ReverseMap();
            }
        }
    }

}