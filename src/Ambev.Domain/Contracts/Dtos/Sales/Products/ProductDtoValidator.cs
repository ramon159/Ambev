using FluentValidation;

namespace Ambev.Domain.Contracts.Dtos.Sales.Products
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Category)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Image)
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .When(x => !string.IsNullOrWhiteSpace(x.Image))
                .WithMessage("Image must be a valid URL");

            RuleFor(x => x.Rating)
                .NotNull()
                .SetValidator(new RatingDtoValidator());
        }
    }
}