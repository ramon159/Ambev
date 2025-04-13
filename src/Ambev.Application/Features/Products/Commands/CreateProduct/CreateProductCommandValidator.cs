using Ambev.Application.Contracts.Dtos.Sales.Products;
using FluentValidation;

namespace Ambev.Application.Features.Products.Commands.CreateProduct
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
