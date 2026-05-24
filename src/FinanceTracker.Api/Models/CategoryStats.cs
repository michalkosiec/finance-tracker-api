using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Validations;

namespace FinanceTracker.Api.Models
{
    public class CategoryStat
    {
        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public decimal TotalExpense { get; set; }

        [Required]
        public int NumberOfTransactions { get; set; }
    }

    public class CategoryStats
    {
        [Required]
        [YearMonth]
        public string Month { get; set; } = string.Empty;

        public IEnumerable<CategoryStat> Categories { get; set; } = new List<CategoryStat>();
    }
}