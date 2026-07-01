using System.ComponentModel.DataAnnotations;

namespace VideoMeeting.API.DTOs
{
    public class CreateMeetingDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
    }
}