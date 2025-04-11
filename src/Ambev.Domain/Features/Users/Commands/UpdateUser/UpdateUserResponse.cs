using Ambev.Domain.Contracts.ViewModels.Users;
using Ambev.Domain.Entities.Authentication;
using AutoMapper;

namespace Ambev.Domain.Features.Users.Commands.UpdateUser
{
    public class UpdateUserResponse : UserVM
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UpdateUserResponse, User>().ReverseMap();
            }
        }
    }
}