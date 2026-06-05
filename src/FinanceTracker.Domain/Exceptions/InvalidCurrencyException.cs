namespace FinanceTracker.Domain.Exceptions
{
    public class InvalidCurrencyException : DomainException
    {
        public InvalidCurrencyException(string? invalidCurrency)
            : base(
                $"The currency code '{invalidCurrency}' is invalid. It must be exactly 3 letters (e.g., USD, PLN)."
            ) { }
    }
}
