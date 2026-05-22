using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Api.Dtos.Categories
{
    public class CategoryReadDto
    {
        [Required]
        public Guid Id {get; set;}

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Name {get; set;} = string.Empty;

        [Required]
        public string Icon {get; set;} = string.Empty;

        [Required]
        public string Color {get; set;} = string.Empty;

        [Required]
        public DateTimeOffset CreatedAt {get; set;}

        [Required]
        public DateTimeOffset UpdatedAt {get; set;}
    }
}