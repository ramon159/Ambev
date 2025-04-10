using Ambev.Infrastructure.Extensions;
using Ambev.Shared.Entities.Sales;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

            // i think i'm wrong, but is that what i understood
            cart.Products.Clear();

            cart.UserId = request.UserId;
            cart.Date = request.Date;

            var cartProducts = request.Products.Select(p =>
            {
                return new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                };
            });

            // due documentation i added new cart items because don't pass Id in request, i don't have no one to ask if this is the correct behavior...

            await _cartProductRepository.DbSet.AddRangeAsync(cartProducts);
            await _cartRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UpdateCartResponse>(cart);

        }
    }
}
