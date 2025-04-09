using Ambev.Domain.Contracts.Dtos.Sales.Products;
using FluentValidation;

namespace Ambev.Domain.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título é obrigatório.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição é obrigatória.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("A categoria é obrigatória.");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("A imagem é obrigatória.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("A imagem deve conter uma URL válida.");

            RuleFor(x => x.Rating)
                .SetValidator(new RatingDtoValidator());
        }
    }
}