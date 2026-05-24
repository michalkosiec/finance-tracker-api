using System.Globalization;
using FinanceTracker.Api.Data;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Api.Repositories
{
    public class TransactionRepo(AppDbContext context) : UserOwnedRepo<Transaction>(context), ITransactionRepo
    {
        public async Task<IEnumerable<Transaction>> GetAllByUserIdAsync(Guid userId, TransactionParameters parameters)
        {
            var query = _context.Transactions.Include(t => t.Category).AsNoTracking().Where(c => c.UserId == userId);

            if (parameters.Month is not null)
            {
                var date = DateTime.SpecifyKind(DateTime.ParseExact(parameters.Month, "yyyy-MM", CultureInfo.InvariantCulture), DateTimeKind.Utc);
                query = query.Where(t => t.Date.Month == date.Month && t.Date.Year == date.Year);
            }

            if (parameters.CategoryId is not null)
            {
                query = query.Where(t => t.CategoryId == parameters.CategoryId);
            }

            if (parameters.Type is not null)
            {
                query = query.Where(t => t.Type == parameters.Type);
            }

            return await query.ToListAsync();
        }

        public async Task<decimal> GetTotalSpendingAsync(Guid userId, Guid categoryId, DateTime month, Guid? excludeTransactionId = null)
        {
           var query = _context.Transactions.Where(t => t.UserId == userId &&
            t.CategoryId == categoryId &&
            t.Date.Year == month.Year &&
            t.Date.Month == month.Month);

           if(excludeTransactionId.HasValue)
            {
                query = query.Where(t => t.Id != excludeTransactionId);
            }

           var totalExpense = await query.Where(t => t.Type == TransactionType.Expense).SumAsync(t => t.Amount);
           var totalIncome = await query.Where(t => t.Type == TransactionType.Income).SumAsync(t => t.Amount);
           
           return totalExpense - totalIncome;
        }

        public async Task<decimal> GetMonthlyTotalAsync(Guid userId, TransactionType type, DateTime date)
        {
            var monthlyQuery = _context.Transactions.AsNoTracking().Where(t => t.UserId == userId && t.Date.Month == date.Month && t.Date.Year == date.Year);
            return await monthlyQuery.Where(t => t.Type == type).SumAsync(t => t.Amount);
        }

        public async Task<CategoryStats> GetCategoryStatsAsync(Guid userId, DateTime date)
        {
            CategoryStats categoryStats = new CategoryStats();

            var statsList = await _context.Transactions.AsNoTracking().Where(t => t.UserId == userId &&
                t.Date.Month == date.Month &&
                t.Date.Year == date.Year &&
                t.Type == TransactionType.Expense)
                .GroupBy(t => t.Category!.Name)
                .Select(g => new CategoryStat { 
                    Category = g.Key, 
                    TotalExpense = g.Sum(t => t.Amount), 
                    NumberOfTransactions = g.Count()})
                .ToListAsync();

            categoryStats.Categories = statsList;

            return categoryStats;
        }

        public async Task<MonthlyStats> GetMonthlyStatsAsync(Guid userId, DateTime date)
        {
            MonthlyStats monthlyStats = new();

            var rawData = await _context.Transactions.AsNoTracking().Where(t => t.UserId == userId && t.Date.Year == date.Year)
                .GroupBy(t => t.Date.Month)
                .Select(g => new { 
                    MonthKey = g.Key, 
                    TotalIncome = g.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount),
                    TotalExpense = g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount),})
                .ToListAsync();

            var monthsList = rawData.Select(m => new MonthlyStat
            {
                Month = $"{date.Year}-{m.MonthKey:D2}",
                TotalIncome = m.TotalIncome,
                TotalExpense = m.TotalExpense,
            }).ToList();

            monthlyStats.Months = monthsList;

            return monthlyStats;
        }
    }

}