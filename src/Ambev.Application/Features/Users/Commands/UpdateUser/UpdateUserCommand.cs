using Ambev.Application.Contracts.Dtos.Users;
using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using Ambev.Domain.Entities.Authentication;
using AutoMapper;
using MediatR;

namespace Ambev.Application.Features.Users.Commands.UpdateUser
{
    [Authorize(Roles = Roles.Customer)]
    public class UpdateUserCommand : UserDto, IRequest<UpdateUserResponse>
    {
        public Guid Id { get; private set; }
        public void SetId(Guid Id)
        {
            this.Id = Id;
        }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UpdateUserCommand, User>().ReverseMap();
            }
        }
    }
}
