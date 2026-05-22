using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Validations;

namespace FinanceTracker.Api.Dtos.Budgets
{
    public class BudgetUpdateDto
    {
        [Required(ErrorMessage = "CategoryId is required.")]
        [RegularExpression(@"^({?)[0-9A-Fa-f]{8}-([0-9A-Fa-f]{4}-){3}[0-9A-Fa-f]{12}(}?)$", ErrorMessage = "Invalid Guid format.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "LimitAmount is required.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "LimitAmount must be greater than 0.")]
        public decimal LimitAmount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Month is required.")]
        [YearMonth]
        public string Month { get; set; } = string.Empty;
    }
}