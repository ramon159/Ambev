using Ambev.Domain.Entities.Authentication;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using Ambev.Domain.Interfaces.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace Ambev.Application.Features.Authentication.Commands.AuthenticateUser
{
    public class AuthUserHandler : IRequestHandler<AuthUserCommand, AuthUserResponse>
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthUserHandler(IRepositoryBase<User> userRepository, ITokenService tokenService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService=tokenService;
            _passwordHasher=passwordHasher;
        }

        public async Task<AuthUserResponse> Handle(AuthUserCommand request, CancellationToken cancellationToken)
        {
            var defaultMessage = "The username or password is Incorrect";

            var user = await _userRepository.DbSet.AsTracking().FirstOrDefaultAsync(u => u.UserName == request.Username);
            if (user == null)
                throw new AuthenticationException(defaultMessage);

            var passwordMatches = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

            if (!passwordMatches)
                throw new AuthenticationException(defaultMessage);

            var token = _tokenService.GenerateToken(user);

            return new AuthUserResponse() { Token = token };
        }
    }
}