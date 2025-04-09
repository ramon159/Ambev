using Ambev.Domain.Dtos;
using MediatR;

namespace Ambev.Domain.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand : IRequest<UpdateProductResponse>
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
