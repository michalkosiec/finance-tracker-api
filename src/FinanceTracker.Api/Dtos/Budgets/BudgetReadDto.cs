using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Api.Dtos.Budgets
{
    public class BudgetReadDto
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public decimal LimitAmount { get; set; }

        [Required]
        public string Month { get; set; } = string.Empty;

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}