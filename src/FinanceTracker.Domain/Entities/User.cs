using FinanceTracker.Domain.Common;
using FinanceTracker.Domain.Exceptions;

namespace FinanceTracker.Domain.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset UpdatedAt { get; private set; }

        private User() { }

        private User(Guid id, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email cannot be null or whitespace.", nameof(email));

            Id = id;
            Name = name;
            Email = email;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public static User Create(string name, string email)
        {
            return new User(Guid.NewGuid(), name, email);
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new DomainException("Name cannot be null or whitespace.", nameof(newName));

            Name = newName;
            UpdateTimestamp();
        }

        public void UpdateEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new DomainException("Email cannot be null or whitespace.", nameof(newEmail));

            Email = newEmail;
            UpdateTimestamp();
        }

        private void UpdateTimestamp()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
