using InstallerManagement.Models;

namespace InstallerManagement.Repositories
{
    public interface IInstallerSupervisorRepository
    {
        Task<InstallerSupervisor> CreateInstallerSupervisorAsync(InstallerSupervisor installerSupervisor);
        Task<InstallerSupervisor> GetInstallerSupervisorAsync(int installerId, int supervisorId);
        Task<List<InstallerSupervisor>> GetInstallerSupervisorsAsync();
        Task<bool> DeleteInstallerSupervisorAsync(int installerId, int supervisorId);
        Task<InstallerSupervisor> UpdateInstallerSupervisorAsync(InstallerSupervisor installerSupervisor);

        // Other repository methods as needed
    }
}