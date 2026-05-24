using System.Linq.Expressions;

namespace FinanceTracker.Api.Repositories.Interfaces
{
    public interface IUserOwnedRepo<T> : IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Guid userId);

        Task<T?> GetByIdAsync(Guid id, Guid userId);

        Task<T?> GetFirstOrDefaultByAsync(Expression<Func<T, bool>> predicate, Guid userId);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, Guid userId);

        Task UpdateAsync(T entity, Guid userId);
        
        Task DeleteAsync(Guid id, Guid userId);
    }
}