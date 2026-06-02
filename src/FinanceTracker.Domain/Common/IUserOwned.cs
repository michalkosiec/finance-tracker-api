namespace FinanceTracker.Domain.Common
{
    public interface IUserOwned
    {
        Guid UserId { get; }
    }
}