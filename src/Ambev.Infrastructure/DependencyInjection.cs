using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using Ambev.Infrastructure.Data;
using Ambev.Infrastructure.Interceptors;
using Ambev.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ambev.Infrastructure
{
    public static class DependencyInjection
    {
        public static void UseInfrastructure(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
            Guard.Against.Null(connectionString, message: "Connection string 'PostgreSqlConnection' not found.");

            builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            builder.Services.AddDbContext<WriteDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            }
            );

            //services.AddDbContext<WriteDbContext>(options =>
            //    options.UseMongoDB(configuration.GetConnectionString("DefaultConnection"))
            //);

            builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            //builder.Services.AddAuthorization();

            //builder.Services.AddAuthentication()
            //    .AddBearerToken(IdentityConstants.BearerScheme);

        }
    }

}
