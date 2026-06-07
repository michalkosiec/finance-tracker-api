namespace FinanceTracker.Application.Common.DTOs.Users
{
    public record UserResponse(
        Guid Id,
        string Name,
        string Email,
        DateTime CreatedAt,
        DateTime UpdatedAt
    ) { }
}
