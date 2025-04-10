using Ambev.Infrastructure.Extensions;
using Ambev.Shared.Entities.Sales;
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

        public UpdateCartHandler(IMapper mapper, IRepositoryBase<Cart> cartRepository)
        {
            _mapper = mapper;
            _cartRepository=cartRepository;
        }

        public async Task<UpdateCartResponse> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {

            var cart = await _cartRepository.GetByIdAsync(
                 request.Id,
                includes: c => c.Include(c => c.Products)
                );

            Guard.Against.NotFound(request.Id, cart);
            cart.Products.Clear();
               
            //await _cartRepository.SaveChangesAsync(cancellationToken);

            cart.Products.AddRange(request.Products.Select(p =>
            {
                return new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                };
            }));

            //await _cartRepository.SaveChangesAsync(cancellationToken);
            cart.UserId = request.UserId;
            cart.Date = request.Date;

            await _cartRepository.SaveChangesAsync(cancellationToken);
            
            //var result = await _cartRepository.UpdateAsync(cart, cancellationToken);

            return _mapper.Map<UpdateCartResponse>(cart);

        }
    }
}
