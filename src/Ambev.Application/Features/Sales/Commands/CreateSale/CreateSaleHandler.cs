using Ambev.Domain.Entities.Authentication;
using Ambev.Domain.Entities.Sales;
using Ambev.Domain.Entities.Sales.Products;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using Ambev.Domain.Interfaces.Infrastructure.Security;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Application.Features.Sales.Commands.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Sale> _saleRepository;
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IUser _user;
        private readonly IRepositoryBase<Product> _productRepository;

        public CreateSaleHandler(IMapper mapper, IRepositoryBase<Sale> saleRepository, IRepositoryBase<User> userRepository, IUser user, IRepositoryBase<Product> productRepository)
        {
            _mapper = mapper;
            _saleRepository=saleRepository;
            _userRepository=userRepository;
            _user=user;
            _productRepository=productRepository;
        }

        public async Task<CreateSaleResponse> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<Sale>(request);
            var user = await GetAuthenticatedUser(cancellationToken);

            sale.CustomerId = user.Id;
            sale.Branch="Online";
            sale.Status = SaleStatus.PendingPayment;
            sale.ShippingAddress = user.Address;


            await ValidateAndBindSaleItems(sale, cancellationToken);

            sale.CalculateTotalAmount();

            var result = await _saleRepository.AddAsync(sale, cancellationToken);
            return _mapper.Map<CreateSaleResponse>(result);
        }

        private async Task ValidateAndBindSaleItems(Sale sale, CancellationToken cancellationToken)
        {
            var productIds = sale.Items.Select(i => i.ProductId).Distinct().ToList();

            var products = await _productRepository.DbSet
                .AsNoTracking()
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            foreach (var item in sale.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                Guard.Against.Null(product, nameof(product), $"Product {item.ProductId} not found");
                item.SetProduct(product);
            }
        }

        private async Task<User> GetAuthenticatedUser(CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(_user.Id, message: "need an authenticated user");
            var userId = Guid.Parse(_user.Id);

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken: cancellationToken);
            Guard.Against.NotFound(userId, user);
            return user;
        }

    }
}
