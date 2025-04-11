using Ambev.Domain.Contracts.Dtos.Sales.Carts;
using Ambev.Domain.Entities.Sales.Carts;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Carts.Commands.CreateCart
{
    public class CreateCartCommand : CartDto, IRequest<CreateCartResponse>
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateCartCommand, Cart>().ReverseMap();
            }
        }
    }
}
