using Ambev.Application.Contracts.ViewModels.Users;
using Ambev.Domain.Entities.Authentication;
using AutoMapper;

namespace Ambev.Application.Features.Users.Queries.GetUser
{
    public class GetUserResponse : UserVM
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GetUserResponse, User>().ReverseMap();
            }
        }
    }
}
