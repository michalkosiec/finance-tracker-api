using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Validations;

namespace FinanceTracker.Api.Models
{
     public class MonthlyStat
    {
        [Required]
        [YearMonth]
        public string Month { get; set; } = string.Empty;

        [Required]
        public decimal TotalIncome { get; set; }

        [Required]
        public decimal TotalExpense { get; set; }
    }
    
    public class MonthlyStats {
        [Required]
        public List<MonthlyStat> Months = new List<MonthlyStat>();
    }
}