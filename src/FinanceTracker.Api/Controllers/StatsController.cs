using FinanceTracker.Api.Dtos.Stats;
using FinanceTracker.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StatsController(IStatsService statsService) : AppControllerBase
    {
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] MonthQueryDto query)
        {
            var statsSummaryReadDto = await statsService.GetSummaryAsync(query, UserId!.Value);

            return Ok(statsSummaryReadDto);
        }

        [HttpGet("by-category")]
        public async Task<IActionResult> GetSummaryByCategory([FromQuery] MonthQueryDto query)
        {
            var categoryStatsRead = await statsService.GetExpensesByCategoryAsync(query, UserId!.Value);

            return Ok(categoryStatsRead);
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyStats([FromQuery] YearQueryDto query)
        {
            var monthlyStatsRead = await statsService.GetMonthlyStatsAsync(query, UserId!.Value);

            return Ok(monthlyStatsRead);
        }
    }
}