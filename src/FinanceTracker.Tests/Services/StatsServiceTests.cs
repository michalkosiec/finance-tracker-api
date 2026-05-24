using AutoMapper;
using FinanceTracker.Api.Dtos.Stats;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories.Interfaces;
using FinanceTracker.Api.Services;
using Moq;

namespace FinanceTracker.Tests.Services
{
    public class StatsServiceTests
    {
        readonly Mock<ITransactionRepo> _repoMock;
        readonly Mock<IMapper> _mapperMock;
        readonly StatsService _statsService;

        public StatsServiceTests()
        {
            _repoMock = new Mock<ITransactionRepo>();
            _mapperMock = new Mock<IMapper>();
            _statsService = new StatsService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetSummaryAsync_ShouldReturnCorrectSummary()
        {
            var userId = Guid.NewGuid();
            var date = new DateTime(2024, 1, 1);
            decimal totalIncome = 1000m;
            decimal totalExpense = 600m;

            _repoMock.Setup(repo => repo.GetMonthlyTotalAsync(userId, TransactionType.Income, date))
                .ReturnsAsync(totalIncome);

            _repoMock.Setup(repo => repo.GetMonthlyTotalAsync(userId, TransactionType.Expense, date))
                .ReturnsAsync(totalExpense);

            var result = await _statsService.GetSummaryAsync(date, userId);

            Assert.NotNull(result);
            Assert.Equal("2024-01", result.Month);
            Assert.Equal(totalIncome, result.TotalIncome);
            Assert.Equal(totalExpense, result.TotalExpense);
            Assert.Equal(totalIncome - totalExpense, result.Balance);
        }

        [Fact]
        public async Task GetExpensesByCategoryAsync_ShouldReturnMappedCategoryStats()
        {
            var userId = Guid.NewGuid();
            var date = new DateTime(2024, 1, 1);

            var categoryStats = new CategoryStats
            {
                Month = "2024-01",
                Categories = new List<CategoryStat> 
                {
                    new CategoryStat { Category = "Food", TotalExpense = 200m, NumberOfTransactions = 5 },
                    new CategoryStat { Category = "Transport", TotalExpense = 100m, NumberOfTransactions = 3 }
                }
            };

            var categoryStatsRead = new CategoryStatsReadDto
            {
                Month = "2024-01",
                Categories = new List<CategoryStatDto> 
                {
                    new CategoryStatDto { Category = "Food", TotalExpense = 200m, NumberOfTransactions = 5 },
                    new CategoryStatDto { Category = "Transport", TotalExpense = 100m, NumberOfTransactions = 3 }
                }
            };

            _repoMock.Setup(repo => repo.GetCategoryStatsAsync(userId, date))
                .ReturnsAsync(categoryStats);

            _mapperMock.Setup(mapper => mapper.Map<CategoryStatsReadDto>(categoryStats))
                .Returns(categoryStatsRead);

            var result = await _statsService.GetExpensesByCategoryAsync(date, userId);

            Assert.NotNull(result);
            Assert.Equal("2024-01", result.Month);
            Assert.Collection(result.Categories,
                item =>
                {
                    Assert.Equal("Food", item.Category);
                    Assert.Equal(200m, item.TotalExpense);
                    Assert.Equal(5, item.NumberOfTransactions);
                },
                item =>
                {
                    Assert.Equal("Transport", item.Category);
                    Assert.Equal(100m, item.TotalExpense);
                    Assert.Equal(3, item.NumberOfTransactions);
                });
        }

        [Fact]
        public async Task GetMonthlyStatsAsync_ShouldFillMissingMonthsWithZeros()
        {
            var userId = Guid.NewGuid();
            var date = new DateTime(2024, 1, 1);

            var dbStats = new MonthlyStats
            {
                Months = new List<MonthlyStat>
                {
                    new MonthlyStat { Month = "2024-02", TotalIncome = 5000m, TotalExpense = 2000m }
                }
            };

            _repoMock.Setup(repo => repo.GetMonthlyStatsAsync(userId, date))
                .ReturnsAsync(dbStats);

            MonthlyStats? capturedStats = null;

            _mapperMock.Setup(mapper => mapper.Map<MonthlyStatsReadDto>(It.IsAny<MonthlyStats>()))
                .Callback<object>(src => capturedStats = src as MonthlyStats)
                .Returns(new MonthlyStatsReadDto());

            await _statsService.GetMonthlyStatsAsync(date, userId);

            Assert.NotNull(capturedStats);
            Assert.Equal(12, capturedStats!.Months.Count);

            var january = capturedStats.Months.Single(m => m.Month == "2024-01");
            Assert.Equal(0m, january.TotalIncome);
            Assert.Equal(0m, january.TotalExpense);

            var february = capturedStats.Months.Single(m => m.Month == "2024-02");
            Assert.Equal(5000m, february.TotalIncome);
            Assert.Equal(2000m, february.TotalExpense);
            
            var december = capturedStats.Months.Single(m => m.Month == "2024-12");
            Assert.Equal(0m, december.TotalIncome);
            Assert.Equal(0m, december.TotalExpense);
        }
    }
}