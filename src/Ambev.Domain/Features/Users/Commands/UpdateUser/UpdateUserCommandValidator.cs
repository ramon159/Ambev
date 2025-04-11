using Ambev.Domain.Contracts.Dtos.Users;
using FluentValidation;

namespace Ambev.Domain.Features.Users.Commands.UpdateUser
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
