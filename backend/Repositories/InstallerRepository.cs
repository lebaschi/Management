using Microsoft.EntityFrameworkCore;
using InstallerManagement.Models;
using InstallerManagement.Data;

namespace InstallerManagement.Repositories
{
    public class InstallerRepository : IInstallerRepository
    {
        private readonly InstallerManagementDbContext _dbContext;

        public InstallerRepository(InstallerManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Installer>> GetInstallersAsync()
        {
            return await _dbContext.Installers.ToListAsync();
        }

        public async Task<Installer> CreateInstallerAsync(Installer installer)
        {
            _dbContext.Installers.Add(installer);
            await _dbContext.SaveChangesAsync();
            return installer;
        }

        public async Task<Installer> UpdateInstallerAsync(Installer installer)
        {
            _dbContext.Entry(installer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return installer;
        }

        public async Task<bool> DeleteInstallerAsync(int id)
        {
            var installer = await _dbContext.Installers.FindAsync(id);
            if (installer == null)
            {
                return false;
            }

            _dbContext.Installers.Remove(installer);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Installer>> GetInstallersWithSupervisorsAsync()
        {
            return await _dbContext.Installers
                .Include(installer => installer.InstallerSupervisors)
                    .ThenInclude(installerSupervisor => installerSupervisor.Supervisor)
                .ToListAsync();
        }

        // Implement other repository methods and logic as needed
    }
}