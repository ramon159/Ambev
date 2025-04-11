using Ambev.Domain.Contracts.ViewModels.Sales.Carts;
using Ambev.Domain.Entities.Sales.Carts;
using AutoMapper;

namespace Ambev.Domain.Features.Carts.Queries.GetCart
{
    public class GetCartResponse : CartVM
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GetCartResponse, Cart>().ReverseMap();
            }
        }
    }
}