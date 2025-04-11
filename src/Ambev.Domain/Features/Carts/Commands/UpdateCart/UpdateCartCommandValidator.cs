using Ambev.Domain.Contracts.Dtos.Sales.Carts;
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
