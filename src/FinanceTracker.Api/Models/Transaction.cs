using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.Api.Models
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public class Transaction : IUserOwned
    {
        [Key]
        [Required]
        public Guid Id {get; set;}

        [Required]
        public Guid UserId {get; set;}
        public User? User {get; set;}

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Transaction name must be between {2} and {1} characters.")]
        public string Name {get; set;} = string.Empty;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Transaction amount must be greater than 0.")]
        public decimal Amount {get; set;}

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be exactly {1} characters.")]
        public string Currency {get; set;} = string.Empty;

        [Required]
        public Guid CategoryId {get; set;}
        public Category? Category {get; set;}

        [Required]
        public DateTime Date {get; set;}

        [Required]
        [EnumDataType(typeof(TransactionType), ErrorMessage = "Invalid transaction type.")]
        public TransactionType Type {get; set;}

        [Required]
        public DateTimeOffset CreatedAt {get; set;}

        [Required]
        public DateTimeOffset UpdatedAt {get; set;}
    }
}