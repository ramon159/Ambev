using Ambev.Shared.Entities.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        DbSet<User> DbSet { get; }

        Task<User> AddAsync(User entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(User entity, CancellationToken cancellationToken = default);
        Task<(List<User> Items, int Count)> GetAllAsync(int page, int pageSize, string sortTerm, Dictionary<string, string>? filters, Func<IQueryable<User>, IQueryable<User>>? includes = null, Func<IQueryable<User>, IQueryable<User>>? selectors = null, CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(Guid id, Func<IQueryable<User>, IQueryable<User>>? includes = null, CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<User> UpdateAsync(User entity, CancellationToken cancellationToken = default);
    }
}