using Ambev.Domain.Contracts.Dtos.Users;
using Ambev.Shared.Entities.Authentication;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Users.Commands.UpdateUser
{
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
