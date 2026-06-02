using FinanceTracker.Domain.Common;
using FinanceTracker.Domain.Exceptions;

namespace FinanceTracker.Domain.Entities
{
    public class Category : IUserOwned, IEntity
    {
        public Guid Id {get; init;}

        public Guid UserId { get; init; }

        public string Name {get; private set;}

        public string Icon {get; private set;}

        public string Color {get; private set;}

        public DateTimeOffset CreatedAt {get; init;}

        public DateTimeOffset UpdatedAt {get; private set;}

        private Category() {}

        public Category(Guid id, Guid userId, string name, string icon, string color)
        {
            if (id == Guid.Empty)
                throw new DomainException("Id cannot be empty.", nameof(id));
            if (userId == Guid.Empty)
                throw new DomainException("UserId cannot be empty.", nameof(userId));
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be null or whitespace.", nameof(name));

            Id = id;
            UserId = userId;
            Name = name;
            Icon = icon;
            Color = color;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new DomainException("Name cannot be null or whitespace.", nameof(newName));

            Name = newName;
            UpdateTimestamp();
        }

        public void UpdateIcon(string newIcon)
        {
            if (string.IsNullOrWhiteSpace(newIcon))
                throw new DomainException("Icon cannot be null or whitespace.", nameof(newIcon));

            Icon = newIcon;
            UpdateTimestamp();
        }

        public void UpdateColor(string newColor)
        {
            Color = newColor;
            UpdateTimestamp();
        }

        private void UpdateTimestamp()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}