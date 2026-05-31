using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.Api.Models
{
    public class Budget : IUserOwned
    {
        [Key]
        public Guid Id {get; set;}

        public Guid UserId {get; set;}
        public User? User {get; set;}

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal LimitAmount { get; set; }

        public DateTime Month { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}