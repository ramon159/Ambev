using FluentValidation;

namespace Ambev.Application.Contracts.Dtos.Sales
{
    public class SaleItemDtoValidator : AbstractValidator<SaleItemDto>
    {
        public SaleItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull()
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Quantity)
                .NotNull()
                .GreaterThan(0)
                .LessThanOrEqualTo(20);
        }
    }
}
