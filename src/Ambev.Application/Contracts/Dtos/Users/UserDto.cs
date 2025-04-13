using Ambev.Domain.Entities.Authentication;
using Ambev.Domain.Enums;
using Ambev.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.Application.Contracts.Dtos.Users
{
    public class UserDto
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public required Name Name { get; set; }
        public required Address Address { get; set; }
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