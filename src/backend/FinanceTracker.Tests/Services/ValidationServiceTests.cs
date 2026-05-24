using Moq;
using FinanceTracker.Api.Repositories.Interfaces;
using FinanceTracker.Api.Services;
using FinanceTracker.Api.Models;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace FinanceTracker.Tests.Services
{
    public class ValidationServiceTests {
        private readonly Mock<IBudgetRepo> _budgetRepoMock;
        private readonly Mock<ITransactionRepo> _transactionRepoMock;
        private readonly Mock<ICategoryRepo> _categoryRepoMock;
        private readonly ValidationService _validationService;

        public ValidationServiceTests()
        {
            _budgetRepoMock = new Mock<IBudgetRepo>();
            _transactionRepoMock = new Mock<ITransactionRepo>();
            _categoryRepoMock = new Mock<ICategoryRepo>();

            _validationService = new ValidationService(
                _budgetRepoMock.Object,
                _transactionRepoMock.Object,
                _categoryRepoMock.Object
            );
        }

        [Fact]
        public async Task AllowCategory_ShouldThrowBadHttpRequestException_WhenCategoryNameAlreadyExists()
        {
            var userId = Guid.NewGuid();
            var category = new Category { Name = "Food" };

            _categoryRepoMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Category, bool>>>(), userId))
                .ReturnsAsync(true);

            var exception = await Assert.ThrowsAsync<BadHttpRequestException>(() =>
                _validationService.AllowCategory(category, userId)
            );

            Assert.Equal("Category with the given name already exists.", exception.Message);
        }

        [Fact]
        public async Task AllowBudget_ShouldThrowException_WhenBudgetAlreadyExistsForMonth()
        {
            var userId = Guid.NewGuid();
            var budget = new Budget 
            { 
                Id = Guid.NewGuid(), 
                CategoryId = Guid.NewGuid(), 
                Month = new DateTime(2026, 5, 1),
                LimitAmount = 1000
            };

            _budgetRepoMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Budget, bool>>>(), userId))
                .ReturnsAsync(true);

            var exception = await Assert.ThrowsAsync<BadHttpRequestException>(() => 
                _validationService.AllowBudget(budget, userId));
            Assert.Equal("Budget for the given month already exists.", exception.Message);
        }

        [Fact]
        public async Task AllowBudget_ShouldThrowException_WhenLimitAmoutBelowAlreadySpentAmount()
        {
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var budget = new Budget
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Month = new DateTime(2026, 5, 1),
                LimitAmount = 100,
            };

            _budgetRepoMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Budget, bool>>>(), userId))
                .ReturnsAsync(false);

            _transactionRepoMock
                .Setup(repo => repo.GetTotalSpendingAsync(userId, categoryId, budget.Month, null))
                .ReturnsAsync(150);

            var exception = await Assert.ThrowsAsync<BadHttpRequestException>(() => 
                _validationService.AllowBudget(budget, userId));

            Assert.Equal("Cannot set the budget limit (100) below the already spent amount (150).", exception.Message);
        }

        [Fact]
        public async Task AllowCategoryDelete_ShouldThrowUnauthorized_WhenUserIsNotOwner()
        {
            var ownerId = Guid.NewGuid();
            var intruderId = Guid.NewGuid();
            var category = new Category { UserId = ownerId };

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => 
                _validationService.AllowCategoryDelete(category, intruderId));
        }

        [Fact]
        public async Task AllowCategoryDelete_ShouldAllow_WhenUserIsOwner()
        {
            var ownerId = Guid.NewGuid();
            var category = new Category { UserId = ownerId };

            var exception = await Record.ExceptionAsync(() => _validationService.AllowCategoryDelete(category, ownerId));
            Assert.Null(exception);
        }

        [Fact]
        public async Task AllowCategoryDelete_ShouldNotThrowException_WhenCategoryHasNoLinkedBudgets()
        {
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, UserId = userId };

            _budgetRepoMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Budget, bool>>>(), userId))
                .ReturnsAsync(false);
            
            var exception = await Record.ExceptionAsync(() => _validationService.AllowCategoryDelete(category, userId));
            Assert.Null(exception);
        }

        [Fact]
        public async Task AllowTransaction_ShouldThrowException_WhenBudgetLimitExceeded()
        {
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();

            var transaction = new Transaction 
            { 
                Id = Guid.NewGuid(),
                CategoryId = categoryId, 
                Amount = 200, 
                Type = TransactionType.Expense 
            };

            var existingBudget = new Budget { LimitAmount = 500, Month = DateTime.Now };

            _categoryRepoMock
                .Setup(r => r.AnyAsync(It.IsAny<Expression<Func<Category, bool>>>(), userId))
                .ReturnsAsync(true);

            _budgetRepoMock
                .Setup(r => r.GetByCategoryAsync(categoryId))
                .ReturnsAsync(existingBudget);

            _transactionRepoMock
                .Setup(r => r.GetTotalSpendingAsync(userId, categoryId, It.IsAny<DateTime>(), transaction.Id))
                .ReturnsAsync(400);

            var ex = await Assert.ThrowsAsync<BadHttpRequestException>(() => 
                _validationService.AllowTransaction(transaction, userId));

            Assert.Contains("exceed the category budget limit", ex.Message);
        }

        [Fact]
        public async Task AllowTransaction_ShouldAllow_WhenBudgetLimitNotExceeded()
        {
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Amount = 50,
                Type = TransactionType.Expense,
            };

            var existingBudget = new Budget { LimitAmount = 500, Month = DateTime.Now };

            _categoryRepoMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Category, bool>>>(), userId))
                .ReturnsAsync(true);
                
            _budgetRepoMock
                .Setup(repo => repo.GetByCategoryAsync(categoryId))
                .ReturnsAsync(existingBudget);

            _transactionRepoMock
                .Setup(repo => repo.GetTotalSpendingAsync(userId, categoryId, It.IsAny<DateTime>(), transaction.Id))
                .ReturnsAsync(400);

            var exception = await Record.ExceptionAsync(() => _validationService.AllowTransaction(transaction, userId));

            Assert.Null(exception);
        }

        [Fact]
        public async Task AllowTransaction_ShouldAllow_WhenTransactionIsIncome()
        {
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Amount = 1000,
                Type = TransactionType.Income,
            };

            var existingBudget = new Budget { LimitAmount = 500, Month = DateTime.Now };

            _categoryRepoMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Category, bool>>>(), userId))
                .ReturnsAsync(true);

            _budgetRepoMock
                .Setup(repo => repo.GetByCategoryAsync(categoryId))
                .ReturnsAsync(existingBudget);

            _transactionRepoMock
                .Setup(repo => repo.GetTotalSpendingAsync(userId, categoryId, It.IsAny<DateTime>(), transaction.Id))
                .ReturnsAsync(400);

            var exception = await Record.ExceptionAsync(() => _validationService.AllowTransaction(transaction, userId));

            Assert.Null(exception);
        }
    }
}