using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Api.Dtos.Stats
{
    public class YearQueryDto
    {
        [Required(ErrorMessage = "Year parameter is required")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Invalid format. Please use yyyy.")]
        public string Year { get; set; } = string.Empty;
    }
}