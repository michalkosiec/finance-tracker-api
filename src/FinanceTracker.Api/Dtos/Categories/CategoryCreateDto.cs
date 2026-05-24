using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Api.Dtos.Categories
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name {get; set;} = string.Empty;

        [Required]
        public string Icon {get; set;} = string.Empty;

        [Required]
        public string Color {get; set;} = string.Empty;
    }
}