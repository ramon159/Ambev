using Ambev.Domain.Contracts.Dtos.Sales;
using FluentValidation;

namespace Ambev.Domain.Features.Sales.Commands.CreateSale
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x)
                .SetValidator(new SaleDtoValidator());
        }
    }
}
