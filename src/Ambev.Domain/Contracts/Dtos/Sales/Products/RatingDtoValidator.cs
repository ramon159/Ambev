using FluentValidation;

namespace Ambev.Domain.Contracts.Dtos.Sales.Products
{
    public class RatingDtoValidator : AbstractValidator<RatingDto>
    {
        public RatingDtoValidator()
        {
            RuleFor(r => r.Rate).NotNull();
            RuleFor(r => r.Count).NotNull();
        }
    }
}
