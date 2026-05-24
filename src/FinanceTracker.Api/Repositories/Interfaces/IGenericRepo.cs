using System.Linq.Expressions;

namespace FinanceTracker.Api.Repositories.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task<T?> GetFirstOrDefaultByAsync(Expression<Func<T, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task CreateAsync(T etity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}