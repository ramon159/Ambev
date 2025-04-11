using Ambev.Domain.Contracts.Dtos.Sales;
using Ambev.Domain.Entities.Sales;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Sales.Commands.CreateSale
{
    public class CreateSaleCommand : SaleDto, IRequest<CreateSaleResponse>
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateSaleCommand, Sale>().ReverseMap();
            }
        }
    }
}
