using Ambev.Domain.Contracts.Dtos.Sales.Carts;
using FluentValidation;

namespace Ambev.Domain.Features.Carts.Commands.CreateCart
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
