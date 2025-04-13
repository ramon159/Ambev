using Ambev.Application.Contracts.Dtos.Sales;
using FluentValidation;

namespace Ambev.Application.Features.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(x => x)
                .SetValidator(new SaleDtoValidator());
        }
    }
}
