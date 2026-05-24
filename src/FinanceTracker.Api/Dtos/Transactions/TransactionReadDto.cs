using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Dtos.Transactions
{
    public class TransactionReadDto
    {
         [Required]
        public Guid Id {get; set;}

        [Required]
        public Guid UserId {get; set;}

        [Required]
        public string Name {get; set;} = string.Empty;

        [Required]
        public decimal Amount {get; set;}

        [Required]
        public string Currency {get; set;} = string.Empty;

        [Required]
        public Guid CategoryId {get; set;}

        [Required]
        public DateTime Date {get; set;}

        [Required]
        public TransactionType Type {get; set;}

        [Required]
        public string Title {get; set;} = string.Empty;

        [Required]
        public DateTimeOffset CreatedAt {get; set;}

        [Required]
        public DateTimeOffset UpdatedAt {get; set;}
    }
}