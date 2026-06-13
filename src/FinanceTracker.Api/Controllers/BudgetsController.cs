using FinanceTracker.Application.Features.Budgets.Commands.CreateBudget;
using FinanceTracker.Application.Features.Budgets.Commands.DeleteBudget;
using FinanceTracker.Application.Features.Budgets.Commands.UpdateBudget;
using FinanceTracker.Application.Features.Budgets.Queries.GetBudgetById;
using FinanceTracker.Application.Features.Budgets.Queries.GetBudgets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    [Authorize]
    public class BudgetsController(ISender mediator) : AppControllerBase(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetBudgets(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetBudgetsQuery(CurrentUserId), cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}", Name = "GetBudgetById")]
        public async Task<IActionResult> GetBudgetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(
                new GetBudgetByIdQuery(CurrentUserId, id),
                cancellationToken
            );

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget(
            [FromBody] CreateBudgetRequest request,
            CancellationToken cancellationToken
        )
        {
            var result = await Mediator.Send(
                new CreateBudgetCommand(
                    CurrentUserId,
                    request.CategoryId,
                    request.LimitAmount,
                    request.Currency,
                    request.Month
                ),
                cancellationToken
            );

            return CreatedAtAction(nameof(GetBudgetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBudget(
            Guid id,
            [FromBody] UpdateBudgetRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new UpdateBudgetCommand(
                id,
                CurrentUserId,
                request.CategoryId,
                request.LimitAmount,
                request.Currency,
                request.Month
            );

            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBudget(Guid id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteBudgetCommand(CurrentUserId, id), cancellationToken);

            return NoContent();
        }

        public record CreateBudgetRequest(
            Guid CategoryId,
            decimal LimitAmount,
            string Currency,
            DateTime Month
        );

        public record UpdateBudgetRequest(
            Guid CategoryId,
            decimal LimitAmount,
            string Currency,
            DateTime Month
        );
    }
}
