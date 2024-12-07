
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServerLibrary.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Points> Points { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }
        /*
        public DbSet<Employee> Employees { get; set; }

        //General Departments / Department / Branch
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }

        // Authentication / Role / System Roles
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        */
    }
}
