using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Validations;

namespace FinanceTracker.Api.Dtos.Stats
{
    public class MonthlyStatReadDto
    {
        [Required]
        [YearMonth]
        public string Month { get; set; } = string.Empty;

        [Required]
        public decimal TotalIncome { get; set; }

        [Required]
        public decimal TotalExpense { get; set; }
    }
    
    public class MonthlyStatsReadDto {
        [Required]
        public List<MonthlyStatReadDto> Months = new List<MonthlyStatReadDto>();
    }
}