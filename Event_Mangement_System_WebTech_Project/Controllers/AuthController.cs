using Event_Mangement_System_WebTech_Project.Models;
using Event_Mangement_System_WebTech_Project.Models.Dto;
using Event_Mangement_System_WebTech_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Event_Mangement_System_WebTech_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //SignUp
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserDto userDto)
        {
            using var hmac = new HMACSHA512();
            var user = new User
            {
                userName = userDto.userName,
                email = userDto.email,
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.passwordHash)),
                userRoleId = userDto.userRoleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok($"Signed up Successfully");
        }

        //Login
        [HttpPost("login")]
        public IActionResult Login(UserDto userDto) 
        {
            var user = _context.Users.FirstOrDefault(x => x.userName == userDto.userName);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            using var hmac = new HMACSHA512(user.passwordHash);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.passwordHash));
            if (!computedHash.SequenceEqual(user.passwordHash)) { return Unauthorized("Invalid Password!"); }

            var token = CreateTokenAsync(userDto);
            return Ok(token);
        }

        //function for Creating JWT Token
        private async Task<string> CreateTokenAsync(UserDto userDto)
        {
            var usr = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.email == userDto.email);


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.userName),
                new Claim(ClaimTypes.Role , usr.Role.roleName)
            };
            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration["TokenKey"] ?? "SuperSecretKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
