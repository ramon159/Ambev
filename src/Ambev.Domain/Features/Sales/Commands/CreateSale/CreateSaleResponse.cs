using Ambev.Domain.Entities.Sales;
using Ambev.Domain.Features.Sales.Queries.GetSale;
using AutoMapper;

namespace Ambev.Domain.Features.Sales.Commands.CreateSale
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
