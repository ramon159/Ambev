using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using Ambev.Domain.Contracts.Dtos.Sales.Products;
using Ambev.Shared.Interfaces.Domain;
using MediatR;

namespace Ambev.Domain.Features.Products.Commands.UpdateProduct
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
        public double Price { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public required RatingDto Rating { get; init; }
    }
}
