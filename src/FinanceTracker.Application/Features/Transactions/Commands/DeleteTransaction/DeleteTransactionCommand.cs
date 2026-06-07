using MediatR;

namespace FinanceTracker.Application.Features.Transactions.Commands.DeleteTransaction
{
    public record DeleteTransactionCommand(Guid UserId, Guid TransactionId) : IRequest<Unit> { }
}
