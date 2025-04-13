using FluentValidation;

namespace Ambev.Application.Contracts.Dtos.Sales.Carts
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
                .GreaterThan(0)
                .LessThanOrEqualTo(20);
        }
    }
}