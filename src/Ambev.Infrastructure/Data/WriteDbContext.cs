using Ambev.Shared.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ambev.Infrastructure.Data
{
    public sealed class WriteDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            GenerateTablesFromAssembly(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);
        }

        private static void GenerateTablesFromAssembly(ModelBuilder builder)
        {
            var entitiesAssembly = Assembly.GetAssembly(typeof(BaseEntity));
            Guard.Against.Null(entitiesAssembly, "entities assembly is null");

            var entityTypes = entitiesAssembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BaseEntity)) && !t.IsAbstract);

            foreach (var entityType in entityTypes)
            {
                builder.Entity(entityType).ToTable(entityType.Name);
            }
        }
    }
}
