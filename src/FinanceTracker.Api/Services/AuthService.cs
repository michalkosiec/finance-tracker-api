using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinanceTracker.Api.Dtos.Users;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories;
using FinanceTracker.Api.Services.Interfaces;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace FinanceTracker.Api.Services
{
    public class AuthService(IUserRepo repo, IMapper mapper, IConfiguration config) : IAuthService
    {
        public async Task<string?> LoginAsync(UserLoginDto userLogin)
        {
            var user = await repo.GetFirstOrDefaultByAsync(u => u.Email == userLogin.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userLogin.Password, user.PasswordHash))
            {
                return null;
            }

            return GenerateJwtToken(user);
        }

        public async Task<bool> RegisterAsync(UserCreateDto userCreate)
        {
            if (await repo.AnyAsync(u => u.Email == userCreate.Email)) {
                return false;
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(userCreate.Password);

            var user = mapper.Map<User>(userCreate);
            user.PasswordHash = passwordHash;

            await repo.CreateAsync(user);

            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}