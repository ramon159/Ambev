using Ambev.Shared.Entities.Sales;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Cart> _cartRepository;

        public UpdateCartHandler(IMapper mapper, IRepositoryBase<Cart> cartRepository)
        {
            _mapper = mapper;
            _cartRepository=cartRepository;
        }

        public async Task<UpdateCartResponse> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {

            var cart = await _cartRepository.GetByIdAsync(
                request.Id
                );

            Guard.Against.NotFound(request.Id, cart);

            cart.UserId = request.UserId;
            cart.Date = request.Date;

            cart.Products = request.Products.Select(p =>
            {
                return new CartProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                };
            }).ToList();

            var result = await _cartRepository.UpdateAsync(cart, cancellationToken);

            return _mapper.Map<UpdateCartResponse>(result);

        }
    }
}
