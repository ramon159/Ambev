using Ambev.Shared.Common.Entities;
using Ambev.Shared.Entities.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ambev.Infrastructure.Data
{
    public sealed class WriteDbContext : IdentityDbContext<User, Role, Guid>
    {
        public WriteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            GenerateTablesFromAssembly(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);
        }

        private static void GenerateTablesFromAssembly(ModelBuilder builder)
        {
            var entitiesAssembly = Assembly.GetAssembly(typeof(IBaseEntity));

            Guard.Against.Null(entitiesAssembly, "entities assembly is null");

            var entityTypes = entitiesAssembly
                .GetTypes()
                .Where(t =>
                    typeof(IBaseEntity).IsAssignableFrom(t) &&
                    !t.IsAbstract &&
                    !t.IsInterface
                );

            foreach (var entityType in entityTypes)
            {
                builder.Entity(entityType).ToTable(entityType.Name);
            }
        }
    }
}
