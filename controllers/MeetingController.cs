using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoMeeting.API.Data;
using VideoMeeting.API.Models;
using VideoMeeting.API.DTOs;

namespace VideoMeeting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MeetingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MeetingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult>CreateMeeting(CreateMeetingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value );
            var meetingCode = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            var meeting = new Meeting
            {
                Title = dto.Title,
                HostId = userId,
                MeetingCode = meetingCode
            };
            _context.Meetings.Add(meeting);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message= "Meeting Created Successfully!",
                MeetingCode = meeting.MeetingCode
            });
        }
    }

        
}