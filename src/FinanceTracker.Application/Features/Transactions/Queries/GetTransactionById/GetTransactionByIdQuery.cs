using FinanceTracker.Application.Common.DTOs.Transactions;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions.Queries.GetTransactionById
{
    public record GetTransactionByIdQuery(Guid UserId, Guid TransactionId)
        : IRequest<TransactionResponse> { }
}
