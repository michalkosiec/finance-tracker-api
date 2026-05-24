using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Repositories.Interfaces
{
    public interface IBudgetRepo : IUserOwnedRepo<Budget>
    {
        Task<Budget?> GetByCategoryAsync(Guid id);
    }
}