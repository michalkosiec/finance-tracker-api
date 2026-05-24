using System.Globalization;
using FinanceTracker.Api.Services;
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
        public async Task<IActionResult> GetSummary([FromQuery] string month)
        {
            if (string.IsNullOrWhiteSpace(month)) return BadRequest("Month parameter is required");

            if (!DateTime.TryParseExact(month, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return BadRequest("Invalid format. Please use yyyy-MM.");

            var statsSummaryReadDto = await statsService.GetSummaryAsync(date, UserId!.Value);

            return Ok(statsSummaryReadDto);
        }

        [HttpGet("by-category")]
        public async Task<IActionResult> GetSummaryByCategory([FromQuery] string month)
        {
            if (string.IsNullOrWhiteSpace(month)) return BadRequest("Month parameter is required");

            if (!DateTime.TryParseExact(month, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return BadRequest("Invalid format. Please use yyyy-MM.");

            var categoryStatsRead = await statsService.GetExpensesByCategoryAsync(date, UserId!.Value);

            return Ok(categoryStatsRead);
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyStats([FromQuery] string year)
        {
            if (string.IsNullOrWhiteSpace(year)) return BadRequest("Year parameter is required");

            if (!DateTime.TryParseExact(year, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return BadRequest("Invalid format. Please use yyyy-MM.");

            var monthlyStatsRead = await statsService.GetMonthlyStatsAsync(date, UserId!.Value);

            return Ok(monthlyStatsRead);
        }
    }
}