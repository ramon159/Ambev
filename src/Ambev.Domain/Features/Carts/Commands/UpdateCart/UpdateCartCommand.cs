using Ambev.Domain.Contracts.Dtos.Sales.Carts;
using Ambev.Shared.Entities.Sales.Carts;
using Ambev.Shared.Interfaces.Domain;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartCommand : CartDto, IRequest<UpdateCartResponse>, IUpdateCommand
    {
        public Guid Id { get; private set; }
        public void SetId(Guid id)
        {
            Id = id;
        }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UpdateCartCommand, Cart>().ReverseMap();
            }
        }
    }
}
