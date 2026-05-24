using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FinanceTracker.Api.Validations
{
    public class YearMonthDayAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string dateString)
            {
                bool isValid = DateTime.TryParseExact(
                    dateString, 
                    "yyyy-MM-dd", 
                    CultureInfo.InvariantCulture, 
                    DateTimeStyles.None, 
                    out _);

                if (isValid)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Invalid date format. Required date format: yyyy-MM-dd (e.g. 2024-05-01).");
        }
    }
}