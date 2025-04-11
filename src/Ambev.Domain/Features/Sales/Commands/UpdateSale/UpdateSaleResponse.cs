using Ambev.Domain.Entities.Sales;
using Ambev.Domain.Features.Sales.Queries.GetSale;
using AutoMapper;

namespace Ambev.Domain.Features.Sales.Commands.UpdateSale
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
