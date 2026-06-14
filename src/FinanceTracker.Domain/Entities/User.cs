using FinanceTracker.Domain.Common;
using FinanceTracker.Domain.Exceptions;

namespace FinanceTracker.Domain.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; init; }
        public string IdentityUserId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset UpdatedAt { get; private set; }

        private User() { }

        private User(Guid id, string identityUserId, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(identityUserId))
                throw new DomainException(
                    "Identity user ID cannot be null or whitespace.",
                    nameof(identityUserId)
                );
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email cannot be null or whitespace.", nameof(email));

            Id = id;
            IdentityUserId = identityUserId;
            Name = name;
            Email = email;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public static User Create(string identityUserId, string name, string email)
        {
            return new User(Guid.NewGuid(), identityUserId, name, email);
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
