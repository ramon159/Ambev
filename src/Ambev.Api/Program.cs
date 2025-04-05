
using Ambev.Api.Middlewares;
using Ambev.Application;
using Ambev.Domain;
using Ambev.Infrastructure;
using Ambev.ServiceDefaults;

namespace Ambev.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.UseServiceDefaults();
            builder.UseInfrastructure();
            builder.UseDomain();
            builder.UseApplication();
            builder.Services.AddScoped<TransactionMiddleware>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.MapDefaultEndpoints();
            app.UseAuthorization();


            app.MapControllers();

            app.UseMiddleware<TransactionMiddleware>();
            app.Run();
        }
    }
}
