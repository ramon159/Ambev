﻿using Ambev.Domain.Contracts.Dtos.Sales;
using FluentValidation;

namespace Ambev.Domain.Features.Sales.Commands.UpdateSale
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
