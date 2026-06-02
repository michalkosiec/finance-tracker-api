using FinanceTracker.Domain.Common;
using FinanceTracker.Domain.Exceptions;
using FinanceTracker.Domain.ValueObjects;

namespace FinanceTracker.Domain.Entities
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public class Transaction : IUserOwned, IEntity
    {
        public Guid Id {get; init;}
        public Guid UserId {get; init;}
        public string Name {get; private set;}
        public Money Amount {get; private set;}
        public Guid CategoryId {get; private set;}
        public DateTime Date {get; private set;}
        public TransactionType Type {get; private set;}
        public DateTimeOffset CreatedAt {get; init;}
        public DateTimeOffset UpdatedAt {get; private set;}

        private Transaction() {}

        public Transaction(Guid id, Guid userId, string name, Money amount, Guid categoryId, DateTime date, TransactionType type)
        {
            if (id == Guid.Empty)
                throw new DomainException("Id cannot be empty.", nameof(id));
            if (userId == Guid.Empty)
                throw new DomainException("UserId cannot be empty.", nameof(userId));
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be null or whitespace.", nameof(name));
            if (categoryId == Guid.Empty)
                throw new DomainException("CategoryId cannot be empty.", nameof(categoryId));

            Id = id;
            UserId = userId;
            Name = name;
            Amount = amount ?? throw new DomainException("Amount cannot be null.", nameof(amount));
            CategoryId = categoryId;
            Date = date;
            Type = type;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateAmount(Money newAmount)
        {
            Amount = newAmount ?? throw new DomainException("Amount cannot be null.", nameof(newAmount));
            UpdateTimestamp();
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new DomainException("Name cannot be null or whitespace.", nameof(newName));

            Name = newName;
            UpdateTimestamp();
        }

        public void UpdateCategory(Guid newCategoryId)
        {
            if (newCategoryId == Guid.Empty)
                throw new DomainException("CategoryId cannot be empty.", nameof(newCategoryId));

            CategoryId = newCategoryId;
            UpdateTimestamp();
        }

        public void UpdateDate(DateTime newDate)
        {
            Date = newDate;
            UpdateTimestamp();
        }

        public void UpdateType(TransactionType newType)
        {
            Type = newType;
            UpdateTimestamp();
        }

        private void UpdateTimestamp()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}