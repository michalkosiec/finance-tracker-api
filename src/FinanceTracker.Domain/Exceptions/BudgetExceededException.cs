namespace FinanceTracker.Domain.Exceptions
{
    public class BudgetExceededException : DomainException
    {
        public BudgetExceededException(string message)
            : base(message) { }
    }
}
