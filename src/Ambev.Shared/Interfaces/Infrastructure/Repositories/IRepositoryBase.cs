
namespace Ambev.Shared.Interfaces.Infrastructure.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {

        DbSet<T> DbSet { get; }

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<List<T>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    }
}