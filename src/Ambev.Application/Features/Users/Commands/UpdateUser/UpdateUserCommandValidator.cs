using Ambev.Application.Contracts.Dtos.Users;
using FluentValidation;

namespace Ambev.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x)
                .SetValidator(new UserDtoValidator());
        }
    }
}
