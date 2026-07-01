namespace VideoMeeting.API.Models
{
    
    public class User
    
    {
        public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
        public string Name { get;set;} = string.Empty;
        public int Id{get;set;}
        public string Email {get;set;} = string.Empty;
        public string PasswordHash{get;set;}=string.Empty;

        public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
    }
    
}