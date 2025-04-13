
using Ambev.Api.Middlewares;
using Ambev.Api.OpenApi;
using Ambev.Application.Behaviours;
using Ambev.Domain;
using Ambev.Infrastructure;
using Ambev.ServiceDefaults;
using AutoMapper.EquivalencyExpression;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Ambev.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
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

            app.UseExceptionHandler(options => { });
            app.UseMiddleware<TransactionMiddleware>();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpContextAccessor();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
            services.AddEndpointsApiExplorer();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();

                options.MapType<Dictionary<string, string>>(() => new OpenApiSchema
                {
                    Type = "object",
                    AdditionalProperties = new OpenApiSchema { Type = "string" }
                });

                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                foreach (string fileName in xmlFiles)
                {
                    string xmlFilePath = Path.Combine(AppContext.BaseDirectory, fileName);
                    if (File.Exists(xmlFilePath))
                        options.IncludeXmlComments(xmlFilePath, includeControllerXmlComments: true);
                }

                options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter an authorization bearer following this example: `Bearer YourTOken`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });

            });

            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddValidatorsFromAssembly(typeof(Domain.DependencyInjection).Assembly);

            services.AddScoped<TransactionMiddleware>();

            services.AddAutoMapper((serviceProvider, cfg) =>
            {
                cfg.AddCollectionMappers();
            }, typeof(Program).Assembly,
                typeof(Application.DependencyInjection).Assembly
                );

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    Assembly.GetExecutingAssembly(),
                    typeof(Program).Assembly,
                    typeof(Application.DependencyInjection).Assembly
                    );
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            });

        }
    }
}
