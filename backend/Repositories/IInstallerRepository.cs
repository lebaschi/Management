using InstallerManagement.Models;

namespace InstallerManagement.Repositories
{
    public interface IInstallerRepository
    {
        Task<List<Installer>> GetInstallersAsync();
        Task<Installer> CreateInstallerAsync(Installer installer);
        Task<Installer> UpdateInstallerAsync(Installer installer);
        Task<bool> DeleteInstallerAsync(int id);
        Task<List<Installer>> GetInstallersWithSupervisorsAsync();
        // Additional repository methods as needed
    }
}