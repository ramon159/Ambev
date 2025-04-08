
using Ambev.Api.Middlewares;
using Ambev.Api.OpenApi;
using Ambev.Domain;
using Ambev.Infrastructure;
using Ambev.ServiceDefaults;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ambev.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            ConfigureServices(builder.Services);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.UseServiceDefaults();
            builder.UseInfrastructure();
            builder.UseDomain();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                options =>
                {
                    var descriptions = app.DescribeApiVersions();

                    // build a swagger endpoint for each discovered API version
                    foreach (var description in descriptions)
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        options.SwaggerEndpoint(url, name);
                    }
                    options.DefaultModelsExpandDepth(1);
                    options.DisplayRequestDuration();
                });
            }
            app.UseHttpsRedirection();

            app.MapDefaultEndpoints();
            app.UseAuthorization();


            app.MapControllers();
            //app.MapIdentityApi<User>();
            app.UseMiddleware<TransactionMiddleware>();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddProblemDetails();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();
                // Mapeia o Dictionary<string, string> para um objeto com propriedades arbitrárias

                options.MapType<Dictionary<string, string>>(() => new OpenApiSchema
                {
                    Type = "object",
                    AdditionalProperties = new OpenApiSchema { Type = "string" }
                });
                //options.OperationFilter<FilterQueryParameterOperationFilter>();
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                foreach (string fileName in xmlFiles)
                {
                    string xmlFilePath = Path.Combine(AppContext.BaseDirectory, fileName);
                    if (File.Exists(xmlFilePath))
                        options.IncludeXmlComments(xmlFilePath, includeControllerXmlComments: true);
                }

                // integrate xml comments
                //var fileName = typeof(Program).Assembly.GetName().Name + ".xml";
                //var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

                //// integrate xml comments
                //options.IncludeXmlComments(filePath);

            });

            services.AddScoped<TransactionMiddleware>();
            services.AddAutoMapper(typeof(Program).Assembly, typeof(Domain.DependencyInjection).Assembly);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(Program).Assembly,
                    typeof(Domain.DependencyInjection).Assembly
                    );
            });

        }
    }
}
