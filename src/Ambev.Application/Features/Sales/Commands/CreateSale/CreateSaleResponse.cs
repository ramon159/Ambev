using Ambev.Application.Features.Sales.Queries.GetSale;
using Ambev.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.Application.Features.Sales.Commands.CreateSale
{
    public class CreateSaleResponse : GetSaleResponse
    {

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateSaleResponse, Sale>().ReverseMap();
            }
        }
    }
}
