using Ambev.Domain.Contracts.Dtos.Sales.Products;
using FluentValidation;

namespace Ambev.Domain.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x)
                .SetValidator(new ProductDtoValidator());
        }
    }
}
