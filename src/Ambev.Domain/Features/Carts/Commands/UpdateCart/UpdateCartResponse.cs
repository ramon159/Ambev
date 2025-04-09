using Ambev.Domain.Features.Carts.Queries.GetCart;
using Ambev.Shared.Entities.Sales;
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
