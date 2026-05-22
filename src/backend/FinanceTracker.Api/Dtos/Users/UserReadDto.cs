using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Api.Dtos.Users
{
    public class UserReadDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateTimeOffset CreatedAt { get; set; }
        
        [Required]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}