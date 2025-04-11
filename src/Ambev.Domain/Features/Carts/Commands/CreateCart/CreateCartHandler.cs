using Ambev.Domain.Entities.Sales.Carts;
using Ambev.Shared.Common.Exceptions;
using Ambev.Shared.Interfaces;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Features.Carts.Commands.CreateCart
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Cart> _cartRepository;
        private readonly IUser _user;

        public CreateCartHandler(IMapper mapper, IRepositoryBase<Cart> cartRepository, IUser user)
        {
            _mapper = mapper;
            _cartRepository=cartRepository;
            _user=user;
        }

        public async Task<CreateCartResponse> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = _mapper.Map<Cart>(request);

            Guard.Against.NullOrEmpty(_user.Id, message: "need an authenticated user");
            cart.UserId = Guid.Parse(_user.Id);

            var cartExists = await _cartRepository.DbSet.FirstOrDefaultAsync(c => c.UserId == cart.UserId, cancellationToken);
            if (cartExists != null)
                throw new BusinessValidationException("The user already has an active cart");

            var result = await _cartRepository.AddAsync(cart, cancellationToken);
            return _mapper.Map<CreateCartResponse>(result);
        }
    }
}
