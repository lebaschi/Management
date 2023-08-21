using InstallerManagement.Models;

namespace InstallerManagement.Services
{
    public interface ISupervisorService
    {
        Task<List<Supervisor>> GetSupervisorsAsync();
        Task<Supervisor> CreateSupervisorAsync(Supervisor supervisor);
        Task<Supervisor> UpdateSupervisorAsync(Supervisor supervisor);
        Task<bool> DeleteSupervisorAsync(int id);
    }
}