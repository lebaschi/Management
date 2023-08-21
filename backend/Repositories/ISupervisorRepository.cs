using InstallerManagement.Models;

namespace InstallerManagement.Repositories
{
    public interface ISupervisorRepository
    {
        Task<List<Supervisor>> GetSupervisorsAsync();
        Task<Supervisor> CreateSupervisorAsync(Supervisor supervisor);
        Task<Supervisor> UpdateSupervisorAsync(Supervisor supervisor);
        Task<bool> DeleteSupervisorAsync(int id);
    }
}