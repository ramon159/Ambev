using Ambev.Domain.Contracts.Dtos.Users;
using Ambev.Shared.ValueObjects;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Ambev.Domain.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(100).WithMessage("Username cannot exceed 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
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
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage("First name cannot exceed 100 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters");
        }
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required")
                .MaximumLength(200).WithMessage("Street cannot exceed 200 characters");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Street number is required")
                .GreaterThan(0).WithMessage("Street number must be greater than zero");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("Zip code is required")
                .Matches(@"^\d{5}-\d{3}$").WithMessage("Invalid zip code format (XXXXX-XXX)")
                .MaximumLength(20).WithMessage("Zip code cannot exceed 20 characters");

            RuleFor(x => x.Latitude)
                .NotEmpty().WithMessage("Latitude is required")
                .Matches(@"^-?([1-8]?[0-9]\.\d+|90\.0+)$").WithMessage("Invalid latitude value")
                .MaximumLength(50).WithMessage("Latitude cannot exceed 50 characters");

            RuleFor(x => x.Longitude)
                .NotEmpty().WithMessage("Longitude is required")
                .Matches(@"^-?((1[0-7][0-9]|[0-9]?[0-9])\.\d+|180\.0+)$").WithMessage("Invalid longitude value")
                .MaximumLength(50).WithMessage("Longitude cannot exceed 50 characters");
        }
    }
}