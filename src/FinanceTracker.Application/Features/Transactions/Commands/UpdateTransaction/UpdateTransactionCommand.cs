using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions.Commands.UpdateTransaction
{
    public record UpdateTransactionCommand(
        Guid Id,
        Guid UserId,
        string Name,
        decimal Amount,
        string Currency,
        Guid CategoryId,
        DateTime Date,
        TransactionType Type
    ) : IRequest<Unit> { }
}
