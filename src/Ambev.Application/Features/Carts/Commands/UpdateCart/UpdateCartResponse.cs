using Ambev.Application.Features.Carts.Queries.GetCart;
using Ambev.Domain.Entities.Sales.Carts;
using AutoMapper;

namespace Ambev.Application.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartResponse : GetCartResponse
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UpdateCartResponse, Cart>().ReverseMap();
            }
        }
    }
}
