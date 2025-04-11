using Ambev.Domain.Entities.Sales.Carts;
using Ambev.Domain.Features.Carts.Queries.GetCart;
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
