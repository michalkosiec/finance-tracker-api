using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories;
using FinanceTracker.Api.Dtos.Users;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace FinanceTracker.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserRepo repo, IMapper mapper) : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await repo.GetAllAsync();
            var usersRead = mapper.Map<IEnumerable<UserReadDto>>(users);

            return Ok(usersRead);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            } else
            {
                var userRead = mapper.Map<UserReadDto>(user);

                return Ok(userRead);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreate)
        {
            var user = mapper.Map<User>(userCreate);
            await repo.CreateAsync(user);
            var userRead = mapper.Map<UserReadDto>(user);

            return CreatedAtAction(nameof(GetUsers), new { id = user }, userRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateDto userUpdate)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            } else
            {
                mapper.Map(userUpdate, user);
                user.UpdatedAt = DateTimeOffset.UtcNow;

                await repo.UpdateAsync(user);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            } else
            {
                await repo.DeleteAsync(id);
                
                return NoContent();
            }
        }
    }
}