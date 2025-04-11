using FluentValidation;

namespace Ambev.Domain.Contracts.Dtos.Sales.Carts
{
    public class CartDtoValidator : AbstractValidator<CartDto>
    {
        public CartDtoValidator()
        {
            RuleFor(x => x.Products)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Must(p => p.Select(p => p.ProductId).Distinct().Count() == p.Count())
                .WithMessage($"Each product must be unique");

            RuleFor(x => x.Products)
                .NotNull()
                .NotEmpty();

            RuleForEach(x => x.Products)
                .SetValidator(new CartProductDtoValidator());
        }
    }
}