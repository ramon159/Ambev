using Ambev.Domain.Entities.Sales;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Application.Features.Sales.Queries.GetSale
{
    public class GetSaleHandle : IRequestHandler<GetSaleCommand, GetSaleResponse>
    {

        private readonly IRepositoryBase<Sale> _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleHandle(IRepositoryBase<Sale> saleRepository, IMapper mapper)
        {
            _saleRepository=saleRepository;
            _mapper=mapper;
        }

        public async Task<GetSaleResponse> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _saleRepository.GetByIdAsync(
                request.Id,
                includes: c => c.Include(c => c.Items),
                cancellationToken: cancellationToken
                );

            Guard.Against.NotFound(request.Id, entity);

            return _mapper.Map<GetSaleResponse>(entity);
        }
    }
}
