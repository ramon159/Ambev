using Ambev.Application.Contracts.Dtos.Sales.Products;
using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using Ambev.Domain.Entities.Sales.Products;
using AutoMapper;
using MediatR;

namespace Ambev.Application.Features.Products.Commands.CreateProduct
{
    [Authorize(Roles = Roles.Admin)]
    public class CreateProductCommand : ProductDto, IRequest<CreateProductResponse>
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateProductCommand, Product>().ReverseMap();
            }
        }
    }
}
