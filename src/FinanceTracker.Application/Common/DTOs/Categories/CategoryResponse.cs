namespace FinanceTracker.Application.Common.DTOs.Categories
{
    public record CategoryResponse(
        Guid Id,
        Guid UserId,
        string Name,
        string Icon,
        string Color,
        DateTime CreatedAt,
        DateTime UpdatedAt
    ) { }
}
