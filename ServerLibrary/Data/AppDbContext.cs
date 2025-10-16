
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Tracing;

namespace ServerLibrary.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventStage> EventStages { get; set; }
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<EventRegistration> EventRegistrations { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }
        
    }
}
