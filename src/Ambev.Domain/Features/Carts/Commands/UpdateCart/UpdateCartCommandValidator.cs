using Ambev.Domain.Validators;
using FluentValidation;

namespace Ambev.Domain.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(x => x)
                .SetValidator(new CartDtoValidator());
        }
    }
}
