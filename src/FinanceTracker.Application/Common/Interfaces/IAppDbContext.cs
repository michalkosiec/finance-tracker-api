using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Budget> Budgets { get; }
        DbSet<Category> Categories { get; }
        DbSet<Transaction> Transactions { get; }
        DbSet<User> Users { get; }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}