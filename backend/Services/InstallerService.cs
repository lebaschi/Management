using Serilog;
using InstallerManagement.Models;
using InstallerManagement.Repositories;

namespace InstallerManagement.Services
{
    public class InstallerService : IInstallerService
    {
        private readonly IInstallerRepository _installerRepository;

        public InstallerService(IInstallerRepository installerRepository)
        {
            _installerRepository = installerRepository ?? throw new ArgumentNullException(nameof(installerRepository));
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

        public async Task<List<Installer>> GetInstallersAsync()
        {
            return await HandleExceptionAndLog(
                async () => await _installerRepository.GetInstallersAsync(),
                "An error occurred while fetching installers."
            );
        }

        public async Task<Installer> CreateInstallerAsync(Installer installer)
        {
            return await HandleExceptionAndLog(
                async () => await _installerRepository.CreateInstallerAsync(installer),
                "An error occurred while creating an installer."
            );
        }

        public async Task<Installer> UpdateInstallerAsync(Installer installer)
        {
            return await HandleExceptionAndLog(
                async () => await _installerRepository.UpdateInstallerAsync(installer),
                "An error occurred while updating an installer."
            );
        }

        public async Task<bool> DeleteInstallerAsync(int id)
        {
            return await HandleExceptionAndLog(
                async () => await _installerRepository.DeleteInstallerAsync(id),
                "An error occurred while deleting an installer."
            );
        }

        public async Task<List<Installer>> GetInstallersWithSupervisorsAsync()
        {
            return await HandleExceptionAndLog(
                async () => await _installerRepository.GetInstallersWithSupervisorsAsync(),
                "An error occurred while fetching installers with supervisors."
            );
        }

        // Implement other service methods and logic as needed
    }
}