using Ambev.Domain.Contracts.ViewModels.Users;
using Ambev.Shared.Entities.Authentication;
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