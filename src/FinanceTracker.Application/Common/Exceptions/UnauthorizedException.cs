namespace FinanceTracker.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base("Authentication failed.") { }

        public UnauthorizedException(string message)
            : base(message) { }
    }
}
