using Ambev.Domain.Validators;
using FluentValidation;

namespace Ambev.Domain.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(x => x.Products)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Must(products => products.Select(p => p.ProductId).Distinct().Count() == products.Count())
                .WithMessage($"Each product must be unique");

            RuleFor(x => x)
                .SetValidator(new CartDtoValidator());
        }
    }
}
