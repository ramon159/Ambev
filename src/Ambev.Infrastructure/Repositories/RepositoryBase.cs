using Ambev.Infrastructure.Data;
using Ambev.Infrastructure.Extensions;
using Ambev.Shared.Common.Entities;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Infrastructure.Repositories
{
    public sealed class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {

        private readonly DbContext _context;
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

        public async Task<List<T>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await DbSet.Paging(page, pageSize)
                .ToListAsync(cancellationToken);
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
