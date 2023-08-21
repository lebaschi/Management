using InstallerManagement.Models;

namespace InstallerManagement.Services
{
    public interface IInstallerService
    {
        Task<List<Installer>> GetInstallersAsync();
        Task<Installer> CreateInstallerAsync(Installer installer);
        Task<Installer> UpdateInstallerAsync(Installer installer);
        Task<bool> DeleteInstallerAsync(int id);
        Task<List<Installer>> GetInstallersWithSupervisorsAsync();
        // Additional service methods as needed
    }
}