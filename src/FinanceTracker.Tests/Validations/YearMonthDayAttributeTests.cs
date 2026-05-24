using FinanceTracker.Api.Validations;

namespace FinanceTracker.Tests.Validations
{
    public class YearMonthDayAttributeTests
    {
        private readonly YearMonthDayAttribute _attribute;

        public YearMonthDayAttributeTests()
        {
            _attribute = new YearMonthDayAttribute();
        }

        [Theory]
        [InlineData("2024-01-01", true)]
        [InlineData("2024-12-31", true)]
        [InlineData("2024-00-01", false)]
        [InlineData("2024-13-01", false)]
        [InlineData("2024-01-00", false)]
        [InlineData("2024-01-32", false)]
        [InlineData("2024-01", false)]
        [InlineData("2024", false)]
        [InlineData("01-2024-01", false)]
        [InlineData("2024-1-1", false)]
        [InlineData("2024-001-001", false)]
        public void IsValid_ShouldReturnExpectedResult(string input, bool expected)
        {
            var result = _attribute.IsValid(input);
            
            Assert.Equal(expected, result);
        }
    }
}