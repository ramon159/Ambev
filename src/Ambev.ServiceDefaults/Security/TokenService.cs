using Ambev.Domain.Interfaces.Infrastructure.Security;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ambev.ServiceDefaults.Security
{
    public sealed class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtconfig;
        public TokenService(IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("Jwt").Get<JwtConfig>();
            Guard.Against.Null(jwtConfig, "Missing Jwt config in appsettings");
            _jwtconfig = jwtConfig;
        }

        public string GenerateToken(IUser user)
        {
            SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }
        private SecurityTokenDescriptor GetTokenDescriptor(IUser user)
        {
            byte[] securityKey = Encoding.UTF8.GetBytes(_jwtconfig.SecretKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(securityKey);
            var expirationHours = DateTime.UtcNow.AddHours(_jwtconfig.TokenDuration);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),

                ]),
                Expires = expirationHours,
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenDescriptor;
        }
    }
}
