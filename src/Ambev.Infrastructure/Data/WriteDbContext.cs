using Ambev.Shared.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ambev.Infrastructure.Data
{
    public sealed class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            GenerateTablesFromAssembly(builder);

            ConfigureSoftDeleteFilters(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);
        }

        private static void ConfigureSoftDeleteFilters(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(WriteDbContext)
                        .GetMethod(nameof(SetSoftDeleteFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                    ?.MakeGenericMethod(entityType.ClrType);

                    method?.Invoke(null, new object[] { builder });
                }
            }
        }

        private static void SetSoftDeleteFilter<TEntity>(ModelBuilder modelBuilder)
            where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
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
