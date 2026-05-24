using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.Api.Models
{
    public class Budget : IUserOwned
    {
        [Key]
        [Required]
        public Guid Id {get; set;}

        [Required]
        public Guid UserId {get; set;}
        public User? User {get; set;}

        [Required]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Limit amount must be greater than 0.")]
        public decimal LimitAmount { get; set; }

        [Required]
        public DateTime Month { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        
        [Required]
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}