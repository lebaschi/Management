using Serilog;
using InstallerManagement.Models;
using InstallerManagement.Repositories;

namespace InstallerManagement.Services
{
    public class InstallerSupervisorService : IInstallerSupervisorService
    {
        private readonly IInstallerSupervisorRepository _installerSupervisorRepository;

        public InstallerSupervisorService(IInstallerSupervisorRepository installerSupervisorRepository)
        {
            _installerSupervisorRepository = installerSupervisorRepository ?? throw new ArgumentNullException(nameof(installerSupervisorRepository));
        }

        private async Task<T> HandleExceptionAndLog<T>(Func<Task<T>> action, string errorMessage)
        {
            try
            {
                return await action.Invoke();
            }
            catch (Exception ex)
            {
                Log.Error(ex, errorMessage);
                throw new ApplicationException(errorMessage, ex);
            }
        }

        public async Task<InstallerSupervisor> CreateInstallerSupervisorAsync(InstallerSupervisor installerSupervisor)
        {
            return await HandleExceptionAndLog(
                async () => await _installerSupervisorRepository.CreateInstallerSupervisorAsync(installerSupervisor),
                "An error occurred while creating the InstallerSupervisor."
            );
        }

        public async Task<InstallerSupervisor> GetInstallerSupervisorAsync(int installerId, int supervisorId)
        {
            return await HandleExceptionAndLog(
                async () => await _installerSupervisorRepository.GetInstallerSupervisorAsync(installerId, supervisorId),
                "An error occurred while fetching the InstallerSupervisor."
            );
        }

        public async Task<List<InstallerSupervisor>> GetInstallerSupervisorsAsync()
        {
            return await HandleExceptionAndLog(
                async () => await _installerSupervisorRepository.GetInstallerSupervisorsAsync(),
                "An error occurred while fetching the list of InstallerSupervisors."
            );
        }

        public async Task<bool> DeleteInstallerSupervisorAsync(int installerId, int supervisorId)
        {
            return await HandleExceptionAndLog(
                async () => await _installerSupervisorRepository.DeleteInstallerSupervisorAsync(installerId, supervisorId),
                "An error occurred while deleting the InstallerSupervisor."
            );
        }

        public async Task<InstallerSupervisor> UpdateInstallerSupervisorAsync(InstallerSupervisor installerSupervisor)
        {
            return await HandleExceptionAndLog(
                async () => await _installerSupervisorRepository.UpdateInstallerSupervisorAsync(installerSupervisor),
                "An error occurred while updating the InstallerSupervisor."
            );
        }

        // Implement other service methods as needed
    }
}