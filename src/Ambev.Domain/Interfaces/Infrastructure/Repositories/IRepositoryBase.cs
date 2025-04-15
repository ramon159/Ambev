using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Interfaces.Infrastructure.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        DbSet<T> DbSet { get; }
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task SynchronizeAsync(IEnumerable<T> entities, System.Linq.Expressions.Expression<Func<T, bool>> predicate, Func<T, object> keySelector, Action<T, T> updateAction, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<(List<T> Items, int Count)> GetAllAsync(int page, int pageSize, string sortTerm, Dictionary<string, string>? filters, Func<IQueryable<T>, IQueryable<T>>? includes = null, Func<IQueryable<T>, IQueryable<T>>? selectors = null, CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? includes = null, bool isTracked = true, CancellationToken cancellationToken = default);
    }
}