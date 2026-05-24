using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Validations;

namespace FinanceTracker.Api.Dtos.Stats
{
    public class StatsSummaryReadDto
    {
        [Required]
        [YearMonth]
        public string Month { get; set; } = string.Empty;

        [Required]
        public decimal TotalIncome { get; set; }

        [Required]
        public decimal TotalExpense { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }
}