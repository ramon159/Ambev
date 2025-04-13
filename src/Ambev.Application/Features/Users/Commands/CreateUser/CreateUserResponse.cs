using Ambev.Application.Contracts.ViewModels.Users;
using Ambev.Domain.Entities.Authentication;
using AutoMapper;

namespace Ambev.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserResponse : UserVM
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateUserResponse, User>().ReverseMap();
            }
        }
    }
}