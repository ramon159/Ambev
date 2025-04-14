using MediatR;

namespace Ambev.Application.Features.Sales.Queries.GetSale
{
    public class GetSaleQuery : IRequest<GetSaleResponse>
    {
        public Guid Id { get; set; }
    }
}
