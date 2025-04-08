using Ambev.Shared.Interfaces;
using Ambev.Shared.Interfaces.Infrastructure.Security;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ambev.ServiceDefaults.Security
{
    public static class AuthenticationExtensions
    {
        public static TBuilder UseJwtAuthentication<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            var authconfig = builder.Configuration.GetSection("Jwt").Get<JwtConfig>();
            Guard.Against.Null(authconfig, message: "Missing Jwt config in appsettings");
            var key = Encoding.ASCII.GetBytes(authconfig.SecretKey);

            builder.Services.AddJwtAuthentication(key);
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IUser, CurrentUser>();
            return builder;
        }
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, byte[] signingKey)
        {

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                        ValidateLifetime = true,
                        LifetimeValidator = LifetimeValidator
                    };
                });

            return services;
        }

        private static bool LifetimeValidator(DateTime? notBefore,
            DateTime? expires,
            SecurityToken securityToken,
            TokenValidationParameters validationParameters)
        {
            return expires != null && expires > DateTime.Now;
        }
    }
}