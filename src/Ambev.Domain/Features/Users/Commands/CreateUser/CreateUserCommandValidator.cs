using Ambev.Domain.Validators;
using FluentValidation;

namespace Ambev.Domain.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x)
                .SetValidator(new UserDtoValidator());
        }
    }
}
