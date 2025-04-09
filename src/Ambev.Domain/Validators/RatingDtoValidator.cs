using Ambev.Domain.Dtos;
using FluentValidation;

namespace Ambev.Domain.Validators
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
