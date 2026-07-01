using System.ComponentModel.DataAnnotations;

namespace VideoMeeting.API.Models
{
    public class Meeting
    {
        public int Id { get; set; }

        [Required]
        public string MeetingCode { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        public int HostId { get; set; }

        public User? Host { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}