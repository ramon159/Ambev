using Ambev.Shared.Entities.Sales.Carts;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Cart> _cartRepository;
        private readonly IRepositoryBase<CartProduct> _cartProductRepository;

        public UpdateCartHandler(IMapper mapper, IRepositoryBase<Cart> cartRepository, IRepositoryBase<CartProduct> cartProductRepository)
        {
            _mapper = mapper;
            _cartRepository=cartRepository;
            this._cartProductRepository=cartProductRepository;
        }

        public async Task<UpdateCartResponse> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByIdAsync(
                 request.Id,
                includes: c => c.Include(c => c.Products)
                );

            Guard.Against.NotFound(request.Id, cart);

            await SynchronizeCartProducts(request, cart);

            await _cartRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UpdateCartResponse>(cart);

        }


        private async Task SynchronizeCartProducts(UpdateCartCommand request, Cart cart)
        {
            var incomingCartProducts = request.Products.Select(p =>
            {
                return new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                };
            });

            foreach (var cartProduct in cart.Products)
            {
                var existingCartProduct = incomingCartProducts.FirstOrDefault(dto => dto.MatchesProduct(cartProduct));

                if (existingCartProduct != null)
                {
                    cartProduct.Quantity = existingCartProduct.Quantity;
                    continue;
                }

                _cartProductRepository.DbSet.Remove(cartProduct);
            }

            var productsToAdd = incomingCartProducts
                .Where(dto => !cart.Products.Any(cp => cp.MatchesProduct(dto)))
                .ToList();

            await _cartProductRepository.DbSet.AddRangeAsync(productsToAdd);
        }
    }
}
