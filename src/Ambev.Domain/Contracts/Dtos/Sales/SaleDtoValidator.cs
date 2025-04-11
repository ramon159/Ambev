using Ambev.Domain.Entities.Sales.Products;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using FluentValidation;

namespace Ambev.Domain.Contracts.Dtos.Sales
{
    public class SaleDtoValidator : AbstractValidator<SaleDto>
    {
        public SaleDtoValidator()
        {
            RuleFor(x => x.Items)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Must(i => i.Select(p => p.ProductId).Distinct().Count() == i.Count())
                .WithMessage($"Each product must be unique");

            RuleFor(x => x.Items)
                .NotEmpty()
                .NotNull();

            RuleForEach(x => x.Items)
                .SetValidator(new SaleItemDtoValidator());

        }
    }
}