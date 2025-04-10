using Ambev.Infrastructure.Data;
using Ambev.Infrastructure.Extensions;
using Ambev.Shared.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WriteDbContext _context;
        public DbSet<User> DbSet { get; }

        public UserRepository(WriteDbContext context)
        {
            _context = context;
            DbSet = context.Set<User>();
        }

        public async Task<User?> GetByIdAsync(Guid id, Func<IQueryable<User>, IQueryable<User>>? includes = null, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Including(includes)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }


        public async Task<(List<User> Items, int Count)> GetAllAsync(int page, int pageSize, string sortTerm, Dictionary<string, string>? filters, Func<IQueryable<User>, IQueryable<User>>? includes = null, Func<IQueryable<User>, IQueryable<User>>? selectors = null, CancellationToken cancellationToken = default)
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
                .AsNoTracking()
                .ToListAsync();
            return (items, count);
        }

        public async Task<User> AddAsync(User entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                Guard.Against.Null(entity);

            var entry = await DbSet.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entry.Entity;
        }

        public async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                Guard.Against.Null(entity);

            var entry = DbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(User entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                Guard.Against.Null(entity);

            var entityEntry = DbSet.Remove(entity);

            var changes = await _context.SaveChangesAsync(cancellationToken);
            return changes > 0;
        }
        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

    }
}
