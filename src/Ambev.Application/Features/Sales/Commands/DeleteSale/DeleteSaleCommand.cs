using MediatR;

namespace Ambev.Application.Features.Sales.Commands.DeleteSale
{
    public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
    {
        public Guid Id { get; set; }
    }
}
