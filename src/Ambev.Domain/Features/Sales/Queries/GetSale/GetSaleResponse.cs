using Ambev.Domain.Contracts.ViewModels.Sales;
using Ambev.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.Domain.Features.Sales.Queries.GetSale
{
    public class GetSaleResponse : SaleVM
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GetSaleResponse, Sale>().ReverseMap();
            }
        }
    }
}