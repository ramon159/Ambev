using Ambev.Domain.Features.Carts.Queries.GetCart;
using Ambev.Shared.Entities.Sales;
using AutoMapper;

namespace Ambev.Domain.Features.Carts.Commands.CreateCart
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
