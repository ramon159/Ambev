using MediatR;

namespace Ambev.Domain.Features.Sales.Queries.GetSale
{
    public class GetSaleCommand : IRequest<GetSaleResponse>
    {
        public Guid Id { get; set; }
    }
}
