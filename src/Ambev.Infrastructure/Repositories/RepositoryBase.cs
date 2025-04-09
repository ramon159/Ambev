using Ambev.Infrastructure.Data;
using Ambev.Infrastructure.Extensions;
using Ambev.Shared.Common.Entities;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Infrastructure.Repositories
{
    public sealed class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {

        private readonly WriteDbContext _context;
        public DbSet<T> DbSet { get; }

        public RepositoryBase(WriteDbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? includes = null, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Including(includes)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }


        public async Task<(List<T> Items, int Count)> GetAllAsync(int page, int pageSize, string sortTerm, Dictionary<string, string>? filters, Func<IQueryable<T>, IQueryable<T>>? includes = null, Func<IQueryable<T>, IQueryable<T>>? selectors = null, CancellationToken cancellationToken = default)
        {
            var query = DbSet.Filtering(filters)
                .Selecting(selectors)
                .Sorting(sortTerm);

            if (includes != null)
            {
                query = includes(query);
            }


            var count = await query.CountAsync(cancellationToken);

            var items = await query
                .Paging(page, pageSize)
                .ToListAsync();
            return (items, count);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                Guard.Against.Null(entity);

            var entry = await DbSet.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entry.Entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                Guard.Against.Null(entity);

            var entry = DbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                Guard.Against.Null(entity);

            var entityEntry = DbSet.Remove(entity);

            var changes = await _context.SaveChangesAsync(cancellationToken);
            return changes > 0;
        }


    }
}
