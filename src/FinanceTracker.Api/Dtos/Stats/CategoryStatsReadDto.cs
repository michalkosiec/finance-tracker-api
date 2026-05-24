using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Validations;

namespace FinanceTracker.Api.Dtos.Stats
{
    public class CategoryStatDto
    {
        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public decimal TotalExpense { get; set; }

        [Required]
        public int NumberOfTransactions { get; set; }
    }

    public class CategoryStatsReadDto
    {
        [Required]
        [YearMonth]
        public string Month { get; set; } = string.Empty;

        public IEnumerable<CategoryStatDto> Categories { get; set; } = new List<CategoryStatDto>();
    }
}