using FinanceTracker.Application.Features.Stats.Queries.GetExpensesByCategory;
using FinanceTracker.Application.Features.Stats.Queries.GetMonthlyStats;
using FinanceTracker.Application.Features.Stats.Queries.GetSummary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    public class StatsController(ISender mediator) : AppControllerBase(mediator)
    {
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary(
            [FromQuery] GetMonthlySummaryRequest request,
            CancellationToken cancellationToken
        )
        {
            var query = new GetSummaryQuery(
                request.Year,
                request.Month,
                request.Currency,
                CurrentUserId
            );
            var result = await Mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("by-category")]
        public async Task<IActionResult> GetSummaryByCategory(
            [FromQuery] GetMonthlySummaryRequest request,
            CancellationToken cancellationToken
        )
        {
            var query = new GetExpensesByCategoryQuery(
                request.Year,
                request.Month,
                request.Currency,
                CurrentUserId
            );
            var result = await Mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyStats(
            [FromQuery] GetMonthlyStatsRequest request,
            CancellationToken cancellationToken
        )
        {
            var query = new GetMonthlyStatsQuery(request.Year, request.Currency, CurrentUserId);
            var result = await Mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }

    public record GetMonthlyStatsRequest(int Year, string Currency);

    public record GetMonthlySummaryRequest(int Year, int Month, string Currency);
}
