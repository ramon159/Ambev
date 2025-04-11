using MediatR;

namespace Ambev.Domain.Features.Sales.Commands.DeleteSale
{
    public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
    {
        public Guid Id { get; set; }
    }
}
