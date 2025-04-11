using Ambev.Domain.Entities.Sales.Carts;
using Ambev.Domain.Features.Carts.Queries.GetCart;
using AutoMapper;

namespace Ambev.Domain.Features.Carts.Commands.UpdateCart
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
