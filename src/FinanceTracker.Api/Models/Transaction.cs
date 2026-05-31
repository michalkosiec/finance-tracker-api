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
        public Guid Id {get; set;}

        public Guid UserId {get; set;}
        public User? User {get; set;}

        public string Name {get; set;} = string.Empty;

        public decimal Amount {get; set;}

        public string Currency {get; set;} = string.Empty;

        public Guid CategoryId {get; set;}
        public Category? Category {get; set;}

        public DateTime Date {get; set;}

        public TransactionType Type {get; set;}

        public DateTimeOffset CreatedAt {get; set;}

        public DateTimeOffset UpdatedAt {get; set;}
    }
}