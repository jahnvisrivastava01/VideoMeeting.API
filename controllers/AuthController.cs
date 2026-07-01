using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoMeeting.API.Data;
using VideoMeeting.API.DTOs;
using VideoMeeting.API.Models;
using VideoMeeting.API.Services;

namespace VideoMeeting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordService _passwordService;

        public AuthController(ApplicationDbContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             var existingUser = await _context.Users.FirstOrDefaultAsync(x=>x.Email == dto.Email);
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

        
    }
}