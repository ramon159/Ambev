using Ambev.Domain.Contracts.ViewModels.Sales.Products;
using Ambev.Domain.Entities.Sales.Products;
using AutoMapper;

namespace Ambev.Domain.Features.Products.Queries.GetProduct
{
    public class GetProductResponse : ProductVM
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GetProductResponse, Product>().ReverseMap();
            }
        }
    }
}
