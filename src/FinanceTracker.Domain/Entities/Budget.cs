using FinanceTracker.Domain.Common;
using FinanceTracker.Domain.Exceptions;
using FinanceTracker.Domain.ValueObjects;

namespace FinanceTracker.Domain.Entities
{
    public class Budget : IUserOwned, IEntity
    {
        public Guid Id {get; init;}
        public Guid UserId {get; init;}
        public Guid CategoryId { get; private set; }
        public Money LimitAmount { get; private set; }
        public DateTime Month { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset UpdatedAt { get; private set; }

        private Budget() {}

        public Budget(Guid id, Guid userId, Guid categoryId, Money limitAmount, DateTime month)
        {
            if (id == Guid.Empty)
                throw new DomainException("Id cannot be empty.", nameof(id));
            if (userId == Guid.Empty)
                throw new DomainException("UserId cannot be empty.", nameof(userId));
            if (categoryId == Guid.Empty)
                throw new DomainException("CategoryId cannot be empty.", nameof(categoryId));
            if (limitAmount.Amount < 0)
                throw new DomainException("LimitAmount cannot be negative.", nameof(limitAmount));

            Id = id;
            UserId = userId;
            CategoryId = categoryId;
            LimitAmount = limitAmount;

            Month = new DateTime(month.Year, month.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateLimitAmount(Money newLimitAmount)
        {
            if (newLimitAmount.Amount < 0)
                throw new DomainException("LimitAmount cannot be negative.", nameof(newLimitAmount));

            LimitAmount = newLimitAmount;
            UpdateTimestamp();
        }

        public void UpdateCategory(Guid newCategoryId)
        {
            if (newCategoryId == Guid.Empty)
                throw new DomainException("CategoryId cannot be empty.", nameof(newCategoryId));

            CategoryId = newCategoryId;
            UpdateTimestamp();
        }

        private void UpdateTimestamp()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void VerifySufficientFunds(Money newTransactionAmount, Money currentTotalExpenses)
        {
            if (newTransactionAmount.Amount < 0)
                throw new DomainException("Transaction amount cannot be negative.", nameof(newTransactionAmount));

            var projectedTotal = currentTotalExpenses.Add(newTransactionAmount);
            if (projectedTotal.Amount > LimitAmount.Amount)
                throw new BudgetExceededException($"Adding this transaction would exceed the budget limit of {LimitAmount.Amount} {LimitAmount.Currency}.");
        }
    }
}
