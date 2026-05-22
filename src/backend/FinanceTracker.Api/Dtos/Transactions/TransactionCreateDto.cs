using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Validations;

namespace FinanceTracker.Api.Dtos.Transactions
{
    public class TransactionCreateDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between {2} and {1} characters.")]
        public string Name {get; set;} = string.Empty;

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount {get; set;}

        [Required(AllowEmptyStrings = false, ErrorMessage = "Currency is required.")]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency must be exactly 3 uppercase letters (e.g., USD, PLN).")]
        public string Currency {get; set;} = string.Empty;

        [Required(ErrorMessage = "CategoryId is required.")]
        [RegularExpression(@"^({?)[0-9A-Fa-f]{8}-([0-9A-Fa-f]{4}-){3}[0-9A-Fa-f]{12}(}?)$", ErrorMessage = "Invalid Guid format.")]
        public Guid CategoryId {get; set;}

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is required.")]
        [YearMonthDay]
        public string Date {get; set;} = string.Empty;

        [Required(ErrorMessage = "Transaction type is required.")]
        [EnumDataType(typeof(TransactionType), ErrorMessage = "Invalid transaction type.")]
        public TransactionType Type {get; set;}
    }
}