using Ambev.Application.Features.Carts.Queries.GetCart;
using Ambev.Domain.Entities.Sales.Carts;
using AutoMapper;

namespace Ambev.Application.Features.Carts.Commands.CreateCart
{
    public class CreateCartResponse : GetCartResponse
    {

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateCartResponse, Cart>().ReverseMap();
            }
        }
    }
}
