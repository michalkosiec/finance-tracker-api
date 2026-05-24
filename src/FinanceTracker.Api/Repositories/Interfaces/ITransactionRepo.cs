using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Repositories.Interfaces
{
    public interface ITransactionRepo : IUserOwnedRepo<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllByUserIdAsync(Guid userId, TransactionParameters parameters);
        Task<decimal> GetTotalSpendingAsync(Guid userId, Guid categoryId, DateTime Month, Guid? excludeTransactionId);

        Task<decimal> GetMonthlyTotalAsync(Guid userId, TransactionType type, DateTime date);

        Task<CategoryStats> GetCategoryStatsAsync(Guid userId, DateTime date);

        Task<MonthlyStats> GetMonthlyStatsAsync(Guid userId, DateTime date);
    }
}