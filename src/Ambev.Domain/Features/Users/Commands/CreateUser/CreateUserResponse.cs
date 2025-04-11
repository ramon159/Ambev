using Ambev.Domain.Contracts.ViewModels.Users;
using Ambev.Domain.Entities.Authentication;
using AutoMapper;

namespace Ambev.Domain.Features.Users.Commands.CreateUser
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