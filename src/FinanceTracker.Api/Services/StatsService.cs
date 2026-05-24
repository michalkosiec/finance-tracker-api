using FinanceTracker.Api.Dtos.Stats;
using FinanceTracker.Api.Models;
using AutoMapper;
using FinanceTracker.Api.Services.Interfaces;
using FinanceTracker.Api.Repositories.Interfaces;

namespace FinanceTracker.Api.Services
{
    public class StatsService(ITransactionRepo repo, IMapper mapper) : IStatsService
    {
         public async Task<StatsSummaryReadDto> GetSummaryAsync(DateTime date, Guid userId)
        {
            var totalIncome = await repo.GetMonthlyTotalAsync(userId, TransactionType.Income, date);
            var totalExpense = await repo.GetMonthlyTotalAsync(userId, TransactionType.Expense, date);

            return new StatsSummaryReadDto
            {
                Month = date.ToString("yyyy-MM"),
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = totalIncome - totalExpense
            };
        }
        public async Task<CategoryStatsReadDto> GetExpensesByCategoryAsync(DateTime date, Guid userId)
        {
            var categoryStats = await repo.GetCategoryStatsAsync(userId, date);
            var categoryStatsRead = mapper.Map<CategoryStatsReadDto>(categoryStats);

            return categoryStatsRead;
        }

        public async Task<MonthlyStatsReadDto> GetMonthlyStatsAsync(DateTime date, Guid userId)
        {
            var montlyStats = await repo.GetMonthlyStatsAsync(userId, date);

            MonthlyStats fullMonthlyStats = new();

            for (int i = 1; i <= 12; i++)
            {
                var monthData = montlyStats.Months.FirstOrDefault(m => m.Month == $"{date.Year}-{i:D2}");

                fullMonthlyStats.Months.Add(new MonthlyStat {
                    Month = $"{date.Year}-{i:D2}",
                    TotalIncome = monthData?.TotalIncome ?? 0m,
                    TotalExpense = monthData?.TotalExpense ?? 0m,
                });
            }

            MonthlyStatsReadDto monthlyStatsRead = mapper.Map<MonthlyStatsReadDto>(fullMonthlyStats);

            return monthlyStatsRead;
        }
    }
}