using Ambev.Domain.Contracts.Dtos.Sales.Carts.Create;
using Ambev.Shared.Entities.Sales;
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
                CreateMap<CreateCartCommand, Product>().ReverseMap();
            }
        }
    }
}
