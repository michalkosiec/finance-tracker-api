using FinanceTracker.Api.Dtos.Users;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.Api.Services;

namespace FinanceTracker.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserService userService) : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersRead = await userService.GetAllUsersAsync();

            return Ok(usersRead);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var userRead = await userService.GetUserByIdAsync(id);

            return userRead == null ? NotFound() : Ok(userRead);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreate)
        {
            var userRead = await userService.CreateUserAsync(userCreate);

            return CreatedAtAction(nameof(GetUsers), new { id = userRead.Id }, userRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateDto userUpdate)
        {
            var result = await userService.UpdateUserAsync(id, userUpdate);

            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await userService.DeleteUserAsync(id);

            return result ? NoContent() : NotFound();
        }
    }
}