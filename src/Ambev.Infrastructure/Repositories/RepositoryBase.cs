using Ambev.Infrastructure.Data;
using Ambev.Infrastructure.Extensions;
using Ambev.Shared.Common.Entities;
using Ambev.Shared.Common.Http;
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

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<PaginedList<T>> GetAllAsync(int page, int pageSize, string sortTerm, Dictionary<string, string> filters, CancellationToken cancellationToken = default)
        {
            var query = DbSet.Filtering(filters)
                .Sorting(sortTerm);

            var count = await query.CountAsync(cancellationToken);

            var items = await query
                .Paging(page, pageSize)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return new PaginedList<T>(items, count, page, pageSize);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            DbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
