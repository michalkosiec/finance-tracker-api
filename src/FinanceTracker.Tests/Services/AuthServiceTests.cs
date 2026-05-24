using System.Linq.Expressions;
using AutoMapper;
using FinanceTracker.Api.Dtos.Users;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories;
using FinanceTracker.Api.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FinanceTracker.Tests.Services
{
    public class AuthServiceTests
    {
        readonly Mock<IUserRepo> _userRepoMock;
        readonly Mock<IMapper> _mapperMock;
        readonly IConfiguration _config;
        readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepoMock = new Mock<IUserRepo>();
            _mapperMock = new Mock<IMapper>();
            var inMemorySettings = new Dictionary<string, string?>
            {
                {"Jwt:Key", "ThisIsASecretKeyForTestingPurposesOnly"},
                {"Jwt:Issuer", "TestIssuer"},
                {"Jwt:Audience", "TestAudience"},
            };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
                
            _authService = new AuthService(_userRepoMock.Object, _mapperMock.Object, _config);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnFalse_WhenEmailAlreadyExists()
        {
            var userCreate = new UserCreateDto
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "Password123!"
            };

            _userRepoMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(true);

            var result = await _authService.RegisterAsync(userCreate);

            Assert.False(result);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnTrue_WhenEmailDoesNotExist()
        {
            var userCreate = new UserCreateDto
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "Password123!"
            };

            _userRepoMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(false);

            var result = await _authService.RegisterAsync(userCreate);

            Assert.True(result);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var userLogin = new UserLoginDto
            {
                Email = "test@example.com",
                Password = "Password123!"
            };

            _userRepoMock
                .Setup(repo => repo.GetFirstOrDefaultByAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User?)null);

            var result = await _authService.LoginAsync(userLogin);

            Assert.Null(result);
        }
    }
}