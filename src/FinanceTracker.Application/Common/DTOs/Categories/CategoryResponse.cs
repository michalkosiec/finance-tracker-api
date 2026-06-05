namespace FinanceTracker.Application.Common.DTOs.Categories
{
    public record CategoryResponse(
        Guid id,
        Guid userId,
        string name,
        string icon,
        string color,
        DateTime createdAt,
        DateTime updatedAt
        )
    { }
}