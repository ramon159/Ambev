using Ambev.Application.Contracts.Dtos.Sales.Products;
using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using Ambev.Domain.Interfaces.Domain;
using MediatR;

namespace Ambev.Application.Features.Products.Commands.UpdateProduct
{
    [Authorize(Roles = Roles.Admin)]
    public record UpdateProductCommand : IRequest<UpdateProductResponse>, IUpdateCommand
    {
        public Guid Id { get; private set; }
        public void SetId(Guid Id)
        {
            this.Id = Id;
        }
        public string Title { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public required RatingDto Rating { get; init; }
    }
}
