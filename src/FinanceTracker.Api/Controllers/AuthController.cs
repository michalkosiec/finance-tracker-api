using FinanceTracker.Api.Dtos.Users;
using FinanceTracker.Api.Services;
using FinanceTracker.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.Api.Services.Interfaces;

namespace FinanceTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService, IUserRepo repo, IMapper mapper) : AppControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            var token = await authService.LoginAsync(userLogin);

            if (token is null)
            {
                return Unauthorized(new { message = "Wrong email or password." });
            }

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userCreate)
        {
            if (!await authService.RegisterAsync(userCreate))
            {
                return BadRequest(new {message = "User is already registered."});
            }
            
            return Ok(new {message = "User was successfully registered."});
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = UserId;
            if (userId is null) return Unauthorized();

            var user = await repo.GetByIdAsync(userId.Value);
            var userRead = mapper.Map<UserReadDto>(user);

            return Ok(userRead);
        }
    }
}