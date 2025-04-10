using Ambev.Domain.Contracts.Dtos.Sales.Carts.Create;
using FluentValidation;

namespace Ambev.Domain.Validators
{
    public class CartProductDtoValidator : AbstractValidator<CartProductDto>
    {
        public CartProductDtoValidator()
        {

            RuleFor(x => x.ProductId)
                .NotNull()
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Quantity)
                .NotNull()
                .GreaterThan(0);
        }
    }
}