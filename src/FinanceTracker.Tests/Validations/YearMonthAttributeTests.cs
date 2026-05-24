using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Validations;

namespace FinanceTracker.Tests.Validations
{
    public class YearMonthAttributeTests
    {
        private readonly YearMonthAttribute _attribute;

        public YearMonthAttributeTests()
        {
            _attribute = new YearMonthAttribute();
        }

        [Theory]
        [InlineData("2024-01", true)]
        [InlineData("2024-12", true)]
        [InlineData("2024-00", false)]
        [InlineData("2024-13", false)]
        [InlineData("2024", false)]
        [InlineData("01-2024", false)]
        [InlineData("2024-1", false)]
        [InlineData("2024-001", false)]
        [InlineData("2024-01-01", false)]
        public void IsValid_ShouldReturnExpectedResult(string input, bool expected)
        {
            var result = _attribute.IsValid(input);
            
            Assert.Equal(expected, result);
        }
    }
}