﻿using Ambev.Domain.Contracts.Dtos.Sales;
using Ambev.Domain.Entities.Sales;
using Ambev.Shared.Interfaces.Domain;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommand : SaleDto, IRequest<UpdateSaleResponse>, IUpdateCommand
    {
        public Guid Id { get; private set; }
        public void SetId(Guid id)
        {
            Id = id;
        }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UpdateSaleCommand, Sale>().ReverseMap();
            }
        }
    }
}
