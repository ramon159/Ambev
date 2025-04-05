namespace Ambev.Shared.Interfaces.Infrastructure.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<Guid> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}