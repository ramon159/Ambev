using Ambev.Application.Contracts.Dtos.Sales.Carts;
using Ambev.Domain.Entities.Sales.Carts;
using Ambev.Domain.Interfaces.Domain;
using AutoMapper;
using MediatR;

namespace Ambev.Application.Features.Carts.Commands.UpdateCart
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
