using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using Ambev.Domain.Contracts.Dtos.Users;
using Ambev.Shared.Entities.Authentication;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Users.Commands.UpdateUser
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
