using Microsoft.AspNetCore.Mvc;
using InstallerManagement.Models;
using InstallerManagement.Services;
using Serilog;
using ILogger = Serilog.ILogger;

namespace InstallerManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstallerController : ControllerBase
    {
        private readonly IInstallerService _installerService;
        private readonly ILogger _logger;

        public InstallerController(IInstallerService installerService)
        {
            _installerService = installerService;
            _logger = Log.ForContext<InstallerController>();
        }

        [HttpGet]
        public async Task<ActionResult<List<Installer>>> GetInstallersAsync()
        {
            try
            {
                var installers = await _installerService.GetInstallersAsync();
                return Ok(installers);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while fetching installers.");
                return StatusCode(500, "An error occurred while fetching installers.");
            }
        }

        [HttpGet("installers-with-supervisors")]
        public async Task<ActionResult<List<Installer>>> GetInstallersWithSupervisorsAsync()
        {
            try
            {
                var installersWithSupervisors = await _installerService.GetInstallersWithSupervisorsAsync();
                return Ok(installersWithSupervisors);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while fetching installers with supervisors.");
                return StatusCode(500, "An error occurred while fetching installers with supervisors.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Installer>> CreateInstallerAsync(Installer installer)
        {
            if (installer == null)
            {
                return BadRequest("Installer data is missing.");
            }

            try
            {
                var createdInstaller = await _installerService.CreateInstallerAsync(installer);
                _logger.Information("Installer created: {@Installer}", createdInstaller);
                return CreatedAtAction(nameof(GetInstallersAsync), createdInstaller);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while creating the installer.");
                return StatusCode(500, "An error occurred while creating the installer.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstallerAsync(int id, Installer installer)
        {
            if (installer == null || id != installer.Id)
            {
                return BadRequest("Invalid installer data or ID mismatch.");
            }

            try
            {
                var updatedInstaller = await _installerService.UpdateInstallerAsync(installer);
                if (updatedInstaller == null)
                {
                    return NotFound($"Installer with ID {id} not found.");
                }

                _logger.Information("Installer updated: {@UpdatedInstaller}", updatedInstaller);
                return Ok(updatedInstaller);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating the installer.");
                return StatusCode(500, "An error occurred while updating the installer.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstallerAsync(int id)
        {
            try
            {
                var deleted = await _installerService.DeleteInstallerAsync(id);
                if (!deleted)
                {
                    return NotFound($"Installer with ID {id} not found.");
                }

                _logger.Information("Installer deleted: {@InstallerId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting the installer.");
                return StatusCode(500, "An error occurred while deleting the installer.");
            }
        }
    }
}