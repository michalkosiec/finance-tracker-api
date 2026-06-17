using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Common.DTOs.Transactions
{
    public record TransactionResponse(
        Guid Id,
        Guid UserId,
        string Name,
        decimal Amount,
        string Currency,
        Guid CategoryId,
        DateTime Date,
        TransactionType Type,
        DateTimeOffset UpdatedAt,
        DateTimeOffset CreatedAt
    ) { }
}
