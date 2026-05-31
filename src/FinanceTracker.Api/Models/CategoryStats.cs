using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Validations;

namespace FinanceTracker.Api.Models
{
    public class CategoryStat
    {
        public string Category { get; set; } = string.Empty;

        public decimal TotalExpense { get; set; }

        public int NumberOfTransactions { get; set; }
    }

    public class CategoryStats
    {
        public string Month { get; set; } = string.Empty;

        public IEnumerable<CategoryStat> Categories { get; set; } = new List<CategoryStat>();
    }
}