using Ambev.Application.Contracts.Dtos.Sales.Carts;
using FluentValidation;

namespace Ambev.Application.Features.Carts.Commands.CreateCart
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x)
                .SetValidator(new CartDtoValidator());
        }
    }
}
