using InstallerManagement.Data;
using InstallerManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InstallerManagement.Repositories
{
    public class InstallerSupervisorRepository : IInstallerSupervisorRepository
    {
        private readonly InstallerManagementDbContext _context;

        public InstallerSupervisorRepository(InstallerManagementDbContext context)
        {
            _context = context;
        }

        public async Task<InstallerSupervisor> CreateInstallerSupervisorAsync(InstallerSupervisor installerSupervisor)
        {
            _context.InstallerSupervisors.Add(installerSupervisor);
            await _context.SaveChangesAsync();
            return installerSupervisor;
        }

        public async Task<InstallerSupervisor> GetInstallerSupervisorAsync(int installerId, int supervisorId)
        {
            return await _context.InstallerSupervisors
                .SingleOrDefaultAsync(i => i.InstallerID == installerId && i.SupervisorID == supervisorId);
        }

        public async Task<List<InstallerSupervisor>> GetInstallerSupervisorsAsync()
        {
            return await _context.InstallerSupervisors.ToListAsync();
        }

        public async Task<bool> DeleteInstallerSupervisorAsync(int installerId, int supervisorId)
        {
            var installerSupervisor = await _context.InstallerSupervisors
                .SingleOrDefaultAsync(i => i.InstallerID == installerId && i.SupervisorID == supervisorId);

            if (installerSupervisor != null)
            {
                _context.InstallerSupervisors.Remove(installerSupervisor);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<InstallerSupervisor> UpdateInstallerSupervisorAsync(InstallerSupervisor installerSupervisor)
        {
            var existingInstallerSupervisor = await _context.InstallerSupervisors
                .SingleOrDefaultAsync(i => i.InstallerID == installerSupervisor.InstallerID && i.SupervisorID == installerSupervisor.SupervisorID);

            if (existingInstallerSupervisor != null)
            {
                existingInstallerSupervisor.Installer = installerSupervisor.Installer; 
                await _context.SaveChangesAsync();
                return existingInstallerSupervisor;
            }

            return null; // Return null if not found
        }

        // Implement other repository methods as needed
    }
}