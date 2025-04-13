using Ambev.Application.Contracts.Dtos.Sales.Products;
using FluentValidation;

namespace Ambev.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
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
                .Must(BeAValidUrl).WithMessage("Image must be a valid URL")
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
