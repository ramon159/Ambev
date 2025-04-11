using Ambev.Domain.Contracts.ViewModels.Users;
using Ambev.Domain.Entities.Authentication;
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
