using Ambev.Infrastructure.Data;
using Ambev.Shared.Common.Entities;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Infrastructure.Repositories
{
    public sealed class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {

        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(WriteDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id).AsTask();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Guid> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
