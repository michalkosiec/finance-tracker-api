using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Validations;

namespace FinanceTracker.Api.Models
{
     public class MonthlyStat
    {
        public string Month { get; set; } = string.Empty;

        public decimal TotalIncome { get; set; }

        public decimal TotalExpense { get; set; }
    }
    
    public class MonthlyStats {
        public List<MonthlyStat> Months = new List<MonthlyStat>();
    }
}