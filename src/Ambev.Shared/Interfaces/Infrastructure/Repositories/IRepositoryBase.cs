
using Ambev.Shared.Common.Http;

namespace Ambev.Shared.Interfaces.Infrastructure.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {

        DbSet<T> DbSet { get; }

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<PaginedList<T>> GetAllAsync(int page, int pageSize, string sortTerm, Dictionary<string, string> filters, Func<IQueryable<T>, IQueryable<T>>? includes = null, CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    }
}