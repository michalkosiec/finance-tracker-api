using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FinanceTracker.Api.Validations
{
    public class YearAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string yearString)
            {
                // Sprawdzamy, czy string to dokładnie "yyyy"
                bool isValid = DateTime.TryParseExact(
                    yearString, 
                    "yyyy", 
                    CultureInfo.InvariantCulture, 
                    DateTimeStyles.None, 
                    out _);

                if (isValid)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Invalid date format. Required date format: yyyy (e.g. 2024).");
        }
    }
}