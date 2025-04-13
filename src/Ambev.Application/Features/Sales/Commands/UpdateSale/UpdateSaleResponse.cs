using Ambev.Application.Features.Sales.Queries.GetSale;
using Ambev.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.Application.Features.Sales.Commands.UpdateSale
{
    public class UpdateSaleResponse : GetSaleResponse
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UpdateSaleResponse, Sale>().ReverseMap();
            }
        }
    }
}
