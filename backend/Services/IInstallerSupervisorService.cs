using InstallerManagement.Models;

namespace InstallerManagement.Services
{
    public interface IInstallerSupervisorService
    {
        Task<InstallerSupervisor> CreateInstallerSupervisorAsync(InstallerSupervisor installerSupervisor);
        Task<InstallerSupervisor> GetInstallerSupervisorAsync(int installerId, int supervisorId);
        Task<List<InstallerSupervisor>> GetInstallerSupervisorsAsync();
        Task<bool> DeleteInstallerSupervisorAsync(int installerId, int supervisorId);
        Task<InstallerSupervisor> UpdateInstallerSupervisorAsync(InstallerSupervisor installerSupervisor);

        // Other service methods as needed
    }
}