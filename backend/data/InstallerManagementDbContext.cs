using Microsoft.EntityFrameworkCore;
using InstallerManagement.Models;

namespace InstallerManagement.Data
{
    public class InstallerManagementDbContext : DbContext
    {
        public InstallerManagementDbContext(DbContextOptions<InstallerManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Installer> Installers { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<InstallerSupervisor> InstallerSupervisors { get; set; }

        // ... other DbSet properties

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstallerSupervisor>()
               .HasKey(isv => new { isv.InstallerID, isv.SupervisorID }); 

            modelBuilder.Entity<InstallerSupervisor>()
                .HasOne(isv => isv.Installer)
                .WithMany(i => i.InstallerSupervisors)
                .HasForeignKey(isv => isv.InstallerID); 

            modelBuilder.Entity<InstallerSupervisor>()
                .HasOne(isv => isv.Supervisor)
                .WithMany(s => s.InstallerSupervisors)
                .HasForeignKey(isv => isv.SupervisorID); 

            // ... other configurations
        }
    }
}