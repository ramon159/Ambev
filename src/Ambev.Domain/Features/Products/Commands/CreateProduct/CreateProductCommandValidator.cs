using Ambev.Domain.Validators;
using FluentValidation;

namespace Ambev.Domain.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
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
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.Image));

            RuleFor(x => x.Rating)
                .NotNull()
                .SetValidator(new RatingDtoValidator());
        }
        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
