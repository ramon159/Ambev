using MediatR;

namespace Ambev.Domain.Features.Authentication.Commands.AuthenticateUser
{
    public class AuthUserCommand : IRequest<AuthUserResponse>
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
