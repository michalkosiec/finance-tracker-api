namespace FinanceTracker.Api.Models
{
    public class TransactionParameters
    {
        public string? Month { get; set; }
        public Guid? CategoryId { get; set; }
        public TransactionType? Type { get; set; }

        // Pagination will be added in the future
    }
}