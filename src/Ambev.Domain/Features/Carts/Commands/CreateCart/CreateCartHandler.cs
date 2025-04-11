using Ambev.Domain.Entities.Sales.Carts;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Carts.Commands.CreateCart
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Cart> _cartRepository;

        public CreateCartHandler(IMapper mapper, IRepositoryBase<Cart> productRepository)
        {
            _mapper = mapper;
            _cartRepository=productRepository;
        }

        public async Task<CreateCartResponse> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Cart>(request);
            var result = await _cartRepository.AddAsync(product, cancellationToken);
            return _mapper.Map<CreateCartResponse>(result);
        }
    }
}
