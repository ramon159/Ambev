using Ambev.Application.Features.Sales.Queries.GetSale;
using Ambev.Application.Models.Http;
using Ambev.Domain.Entities.Sales;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Application.Features.Sales.Queries.GetSaleWithPagination
{
    public class GetAllSalesHandle : IRequestHandler<GetAllSalesQuery, PaginedList<GetSaleResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Sale> _saleRepository;

        public GetAllSalesHandle(IMapper mapper, IRepositoryBase<Sale> saleRepository)
        {
            _mapper=mapper;
            _saleRepository=saleRepository;
        }
        public async Task<PaginedList<GetSaleResponse>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var paginedResult = await _saleRepository.GetAllAsync(
                page: request.Page,
                pageSize: request.Size,
                sortTerm: request.Order,
                filters: request.Filters,
                includes: p => p.Include(p => p.Items),
                cancellationToken: cancellationToken
            );

            var items = _mapper.Map<List<GetSaleResponse>>(paginedResult.Items);

            return new PaginedList<GetSaleResponse>(items, paginedResult.Count, request.Page, request.Size);
        }
    }
}
