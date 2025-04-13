using Ambev.Application.Contracts.Dtos.Sales;
using FluentValidation;

namespace Ambev.Application.Features.Sales.Commands.CreateSale
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
