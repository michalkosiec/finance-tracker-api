using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Api.Dtos.Stats
{
    public class MonthQueryDto
    {
        [Required(ErrorMessage = "Month parameter is required")]
        [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "Invalid format. Please use yyyy-MM.")]
        public string Month { get; set; } = string.Empty;
    }
}