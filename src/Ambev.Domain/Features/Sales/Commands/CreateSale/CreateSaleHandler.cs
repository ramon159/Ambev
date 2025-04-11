using Ambev.Domain.Entities.Authentication;
using Ambev.Domain.Entities.Sales;
using Ambev.Domain.Entities.Sales.Products;
using Ambev.Shared.Interfaces;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;

namespace Ambev.Domain.Features.Sales.Commands.CreateSale
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
            Guid userId;
            Guid.TryParse(_user.Id, out userId);

            Guard.Against.Null(userId, "invalid user id");

            var user = await _userRepository.GetByIdAsync(userId);

            Guard.Against.NotFound(userId, user);

            var sale = _mapper.Map<Sale>(request);

            sale.CustomerId = userId;
            sale.Branch="Online";
            sale.Status = SaleStatus.PendingPayment;
            sale.ShippingAddress = user.Address;

            // forcing bug
            sale.Items.ForEach(item => item.ProductId = Guid.NewGuid());

            var result = await _saleRepository.AddAsync(sale, cancellationToken);
            return _mapper.Map<CreateSaleResponse>(result);
        }
    }
}
