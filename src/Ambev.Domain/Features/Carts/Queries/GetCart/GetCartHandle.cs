using Ambev.Shared.Entities.Sales;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Features.Carts.Queries.GetCart
{
    public class GetCartHandle : IRequestHandler<GetCartCommand, GetCartResponse>
    {

        private readonly IRepositoryBase<Cart> _cartRepository;
        private readonly IMapper _mapper;

        public GetCartHandle(IRepositoryBase<Cart> cartRepository, IMapper mapper)
        {
            _cartRepository=cartRepository;
            _mapper=mapper;
        }

        public async Task<GetCartResponse> Handle(GetCartCommand request, CancellationToken cancellationToken)
        {
            var entity = await _cartRepository.GetByIdAsync(
                request.Id,
                includes: c => c.Include(c => c.Products),
                cancellationToken: cancellationToken
                );

            Guard.Against.NotFound(request.Id, entity);

            return _mapper.Map<GetCartResponse>(entity);
        }
    }
}
