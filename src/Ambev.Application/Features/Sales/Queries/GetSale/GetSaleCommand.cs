using MediatR;

namespace Ambev.Application.Features.Sales.Queries.GetSale
{
    public class GetSaleCommand : IRequest<GetSaleResponse>
    {
        public Guid Id { get; set; }
    }
}
