using FinanceTracker.Domain.Exceptions;

namespace FinanceTracker.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; }

        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new DomainException("Amount cannot be negative.", nameof(amount));
            if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
                throw new InvalidCurrencyException(currency);

            Amount = amount;
            Currency = currency.ToUpper();
        }
        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot add money with different currencies.");

            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot subtract money with different currencies.");

            return new Money(Amount - other.Amount, Currency);
        }
    }
}