using Ambev.Domain.Contracts.Dtos.Sales.Carts.Create;
using FluentValidation;

namespace Ambev.Domain.Validators
{
    public class CartDtoValidator : AbstractValidator<CartDto>
    {
        public CartDtoValidator()
        {
            //RuleFor(x => x.UserId)
            //.NotEmpty();

            RuleFor(x => x.Products)
                .NotNull()
                .Must(p => p.Count > 0);

            RuleForEach(x => x.Products)
                .SetValidator(new CartProductDtoValidator());
        }
    }
}