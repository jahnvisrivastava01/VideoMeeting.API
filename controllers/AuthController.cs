using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoMeeting.API.Data;
using VideoMeeting.API.DTOs;
using VideoMeeting.API.Models;
using VideoMeeting.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace VideoMeeting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext context, PasswordService passwordService, JwtService jwtService)
        {
            _context = context;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             var existingUser = await _context.Users
    .FirstOrDefaultAsync(x => x.Email == dto.Email);
    if (existingUser != null){
        return Conflict("Email already exists.");
        }
             var user= new User
             {
                 Name= dto.Name,
                 Email=dto.Email,
                 PasswordHash = _passwordService.HashPassword(dto.Password)
             };
             _context.Users.Add(user);
             await _context.SaveChangesAsync();
             return Ok("User Registered Successfully!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            var isPasswordValid = _passwordService.VerifyPassword(user.PasswordHash, dto.Password);
            if (!isPasswordValid)
            {
                return Unauthorized("Invalid email or password.");
            }
            
            var token = _jwtService.GenerateToken(user);

return Ok(new
{
    Token = token
});
        }
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok ("Welcome ! You are authenticated.");
        }

        
    }
}