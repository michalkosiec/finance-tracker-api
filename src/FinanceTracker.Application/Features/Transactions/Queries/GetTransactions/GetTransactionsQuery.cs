using FinanceTracker.Application.Common.DTOs.Transactions;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions.Queries.GetTransactions
{
    public record GetTransactionsQuery(Guid UserId)
        : IRequest<IReadOnlyCollection<TransactionResponse>> { }
}
