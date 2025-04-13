using Ambev.Application.Contracts.Dtos.Sales.Carts;
using FluentValidation;

namespace Ambev.Application.Features.Carts.Commands.UpdateCart
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
