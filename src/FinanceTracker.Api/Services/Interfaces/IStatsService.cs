using FinanceTracker.Api.Dtos.Stats;

namespace FinanceTracker.Api.Services.Interfaces
{
    public interface IStatsService
    {
        Task<StatsSummaryReadDto> GetSummaryAsync(DateTime date, Guid userId);
        Task<CategoryStatsReadDto> GetExpensesByCategoryAsync(DateTime date, Guid userId);
        Task<MonthlyStatsReadDto> GetMonthlyStatsAsync(DateTime date, Guid userId);
    }
}