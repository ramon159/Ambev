using Ambev.Domain.Features.Carts.Queries.GetCart;
using Ambev.Shared.Common.Http;
using Ambev.Shared.Entities.Sales.Carts;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Features.Carts.Queries.GetCartWithPagination
{
    public class GetAllCartsHandle : IRequestHandler<GetAllCartsCommand, PaginedList<GetCartResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Cart> _cartRepository;

        public GetAllCartsHandle(IMapper mapper, IRepositoryBase<Cart> cartRepository)
        {
            _mapper=mapper;
            _cartRepository=cartRepository;
        }
        public async Task<PaginedList<GetCartResponse>> Handle(GetAllCartsCommand request, CancellationToken cancellationToken)
        {
            var paginedResult = await _cartRepository.GetAllAsync(
                page: request.Page,
                pageSize: request.Size,
                sortTerm: request.Order,
                filters: request.Filters,
                includes: p => p.Include(p => p.Products),
                cancellationToken: cancellationToken
            );

            var items = _mapper.Map<List<GetCartResponse>>(paginedResult.Items);

            return new PaginedList<GetCartResponse>(items, paginedResult.Count, request.Page, request.Size);
        }
    }
}
