using Serilog;
using InstallerManagement.Models;
using InstallerManagement.Repositories;

namespace InstallerManagement.Services
{
    public class SupervisorService : ISupervisorService
    {
        private readonly ISupervisorRepository _supervisorRepository;

        public SupervisorService(ISupervisorRepository supervisorRepository)
        {
            _supervisorRepository = supervisorRepository ?? throw new ArgumentNullException(nameof(supervisorRepository));
        }

        public async Task<List<Supervisor>> GetSupervisorsAsync()
        {
            Log.Information("Getting supervisors...");
            try
            {
                return await _supervisorRepository.GetSupervisorsAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while getting supervisors.");
                throw;
            }
        }

        public async Task<Supervisor> CreateSupervisorAsync(Supervisor supervisor)
        {
            Log.Information("Creating supervisor: {@Supervisor}", supervisor);
            try
            {
                return await _supervisorRepository.CreateSupervisorAsync(supervisor);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while creating supervisor: {@Supervisor}", supervisor);
                throw;
            }
        }

        public async Task<Supervisor> UpdateSupervisorAsync(Supervisor supervisor)
        {
            Log.Information("Updating supervisor: {@Supervisor}", supervisor);
            try
            {
                return await _supervisorRepository.UpdateSupervisorAsync(supervisor);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating supervisor: {@Supervisor}", supervisor);
                throw;
            }
        }

        public async Task<bool> DeleteSupervisorAsync(int id)
        {
            Log.Information("Deleting supervisor with ID: {Id}", id);
            try
            {
                return await _supervisorRepository.DeleteSupervisorAsync(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting supervisor with ID: {Id}", id);
                throw;
            }
        }
    }
}