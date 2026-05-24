using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Api.Models
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "User name must be between {2} and {1} characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed {1} characters.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255, ErrorMessage = "Password hash cannot exceed {1} characters.")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(20, ErrorMessage = "Role name cannot exceed {1} characters.")]
        public string Role { get; set; } = "User";

        [Required]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}