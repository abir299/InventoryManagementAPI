using InventoryManagementAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(InventoryManagementDbContext _context) : ControllerBase
    {
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users
                .FirstOrDefault(e => e.Email == email && e.PasswordHash == password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var role = user.Role.Trim();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("InventoryManagementSecretKey12345"));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                role = role
            });
        }
    }
}