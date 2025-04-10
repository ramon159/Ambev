using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using Ambev.Domain.Contracts.Dtos.Users;
using Ambev.Shared.Entities.Authentication;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Users.Commands.CreateUser
{
    [Authorize(Roles = Roles.Customer)]
    public class CreateUserCommand : UserDto, IRequest<CreateUserResponse>
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateUserCommand, User>().ReverseMap();
            }
        }
    }
}
