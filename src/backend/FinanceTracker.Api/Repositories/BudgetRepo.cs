using FinanceTracker.Api.Data;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Api.Repositories
{
    public class BudgetRepo(AppDbContext context) : UserOwnedRepo<Budget>(context), IBudgetRepo
    {
        public async Task<Budget?> GetByCategoryAsync(Guid id)
        {
            return await _context.Budgets.FirstOrDefaultAsync(b => b.CategoryId == id);
        }
    }
}