using Ambev.Application.Contracts.Dtos.Users;
using FluentValidation;

namespace Ambev.Application.Features.Users.Commands.CreateUser
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
