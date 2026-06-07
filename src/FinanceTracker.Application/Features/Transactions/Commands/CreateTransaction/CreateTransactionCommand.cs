using FinanceTracker.Application.Common.DTOs.Transactions;
using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions.Commands.CreateTransaction
{
    public record CreateTransactionCommand(
        Guid UserId,
        string Name,
        decimal Amount,
        string Currency,
        Guid CategoryId,
        DateTime Date,
        TransactionType Type
    ) : IRequest<TransactionResponse> { }
}
