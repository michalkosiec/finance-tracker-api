using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Api.Models
{
    public class Category : IUserOwned
    {
        [Key]
        public Guid Id {get; set;}

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public string Name {get; set;} = string.Empty;

        public string Icon {get; set;} = string.Empty;

        public string Color {get; set;} = string.Empty;

        public DateTimeOffset CreatedAt {get; set;} = DateTimeOffset.UtcNow;

        public DateTimeOffset UpdatedAt {get; set;} = DateTimeOffset.UtcNow;
    }
}