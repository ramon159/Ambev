using Ambev.Infrastructure.Data;
using Ambev.Infrastructure.Repositories;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
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

            builder.Services.AddDbContext<WriteDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            //services.AddDbContext<WriteDbContext>(options =>
            //    options.UseMongoDB(configuration.GetConnectionString("DefaultConnection"))
            //);

            builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));


        }
    }

}
