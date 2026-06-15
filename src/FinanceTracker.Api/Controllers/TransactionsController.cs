using FinanceTracker.Application.Features.Transactions.Commands.CreateTransaction;
using FinanceTracker.Application.Features.Transactions.Commands.DeleteTransaction;
using FinanceTracker.Application.Features.Transactions.Commands.UpdateTransaction;
using FinanceTracker.Application.Features.Transactions.Queries.GetTransactionById;
using FinanceTracker.Application.Features.Transactions.Queries.GetTransactions;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    public class TransactionsController(ISender mediator) : AppControllerBase(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetTransactions(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(
                new GetTransactionsQuery(CurrentUserId),
                cancellationToken
            );

            return Ok(result);
        }

        [HttpGet("{id:guid}", Name = "GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var result = await Mediator.Send(
                new GetTransactionByIdQuery(CurrentUserId, id),
                cancellationToken
            );

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(
            [FromBody] CreateTransactionRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateTransactionCommand(
                CurrentUserId,
                request.Name,
                request.Amount,
                request.Currency,
                request.CategoryId,
                request.Date,
                request.Type
            );

            var result = await Mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetTransactionById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTransaction(
            Guid id,
            [FromBody] UpdateTransactionRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new UpdateTransactionCommand(
                id,
                CurrentUserId,
                request.Name,
                request.Amount,
                request.Currency,
                request.CategoryId,
                request.Date,
                request.Type
            );

            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTransaction(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            await Mediator.Send(new DeleteTransactionCommand(CurrentUserId, id), cancellationToken);

            return NoContent();
        }

        public record CreateTransactionRequest(
            string Name,
            decimal Amount,
            string Currency,
            Guid CategoryId,
            string Date,
            TransactionType Type
        );

        public record UpdateTransactionRequest(
            string Name,
            decimal Amount,
            string Currency,
            Guid CategoryId,
            string Date,
            TransactionType Type
        );
    }
}
