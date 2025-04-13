using Ambev.Domain.ValueObjects;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Ambev.Application.Contracts.Dtos.Users
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number");

            RuleFor(x => x.Name)
                .SetValidator(new NameValidator());

            RuleFor(x => x.Address)
                .SetValidator(new AddressValidator());

            RuleFor(x => x.Phone)
                    .Matches(new Regex(@"^(\+?\d{1,3}[- ]?)?\(?\d{2}\)?[- ]?\d{4,5}-?\d{4}$"))
                    .WithMessage("Invalid international phone format")
                    .MaximumLength(20)
                    .When(x => !string.IsNullOrEmpty(x.Phone));
        }
    }

    public class NameValidator : AbstractValidator<Name>
    {
        public NameValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Street)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Number)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .Matches(@"^\d{5}-\d{3}$").WithMessage("Invalid zip code format (XXXXX-XXX)")
                .MaximumLength(20);

            RuleFor(x => x.Latitude)
                .NotEmpty()
                .Matches(@"^-?([1-8]?[0-9]\.\d+|90\.0+)$")
                .MaximumLength(50);

            RuleFor(x => x.Longitude)
                .NotEmpty()
                .Matches(@"^-?((1[0-7][0-9]|[0-9]?[0-9])\.\d+|180\.0+)$")
                .MaximumLength(50);
        }
    }
}