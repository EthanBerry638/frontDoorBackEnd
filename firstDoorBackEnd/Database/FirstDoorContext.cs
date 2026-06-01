using firstDoorBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace firstDoorBackEnd.Database
{
    public class FirstDoorContext : DbContext
    {
        public DbSet<SavedJob> SavedJobs { get; set; }
        public DbSet<JobNote> JobNotes { get; set; }
        public FirstDoorContext(DbContextOptions<FirstDoorContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
