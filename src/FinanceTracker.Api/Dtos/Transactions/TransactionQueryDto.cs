using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Dtos.Transactions
{
    public class TransactionQueryDto
    {
        public string? Month { get; set; }
        public Guid? CategoryId { get; set; }
        public TransactionType? Type { get; set; }

        // Pagination will be added in the future
    }
}