using Microsoft.EntityFrameworkCore;
using prac.Models;

namespace prac.Data
{
    public class TimeTrackingContext : DbContext
    {
        public TimeTrackingContext(DbContextOptions<TimeTrackingContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<prac.Models.pracTask> Tasks { get; set; }
        public DbSet<Entry> Entries { get; set; }
    }
}
