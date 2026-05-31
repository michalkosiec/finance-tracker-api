using FinanceTracker.Api.Dtos.Stats;

namespace FinanceTracker.Api.Services.Interfaces
{
    public interface IStatsService
    {
        Task<StatsSummaryReadDto> GetSummaryAsync(MonthQueryDto query, Guid userId);
        Task<CategoryStatsReadDto> GetExpensesByCategoryAsync(MonthQueryDto query, Guid userId);
        Task<MonthlyStatsReadDto> GetMonthlyStatsAsync(YearQueryDto query, Guid userId);
    }
}