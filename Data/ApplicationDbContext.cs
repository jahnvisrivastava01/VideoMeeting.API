using Microsoft.EntityFrameworkCore;
using VideoMeeting.API.Models;

namespace VideoMeeting.API.Data
{
    public class ApplicationDbContext : DbContext
    
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }
        public DbSet<User>Users{get;set;}
    }
}