using FluentValidation;

namespace Ambev.Application.Contracts.Dtos.Sales.Products
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
