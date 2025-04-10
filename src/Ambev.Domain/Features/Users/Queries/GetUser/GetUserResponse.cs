using Ambev.Domain.Contracts.ViewModels.Users;
using Ambev.Shared.Entities.Authentication;
using AutoMapper;

namespace Ambev.Domain.Features.Users.Queries.GetUser
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
