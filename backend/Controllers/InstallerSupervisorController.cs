using Microsoft.AspNetCore.Mvc;
using InstallerManagement.Models;
using InstallerManagement.Services;
using Serilog;
using ILogger = Serilog.ILogger;

namespace InstallerManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstallerSupervisorController : ControllerBase
    {
        private readonly IInstallerSupervisorService _installerSupervisorService;
        private readonly ILogger _logger;

        public InstallerSupervisorController(IInstallerSupervisorService installerSupervisorService)
        {
            _installerSupervisorService = installerSupervisorService;
            _logger = Log.ForContext<InstallerSupervisorController>();
        }

        [HttpPost]
        public async Task<ActionResult<InstallerSupervisor>> CreateInstallerSupervisorAsync(InstallerSupervisor installerSupervisor)
        {
            try
            {
                var createdInstallerSupervisor = await _installerSupervisorService.CreateInstallerSupervisorAsync(installerSupervisor);
                _logger.Information("InstallerSupervisor created: {@InstallerSupervisor}", createdInstallerSupervisor);
                return CreatedAtAction(nameof(GetInstallerSupervisorAsync), new { installerId = createdInstallerSupervisor.InstallerID, supervisorId = createdInstallerSupervisor.SupervisorID }, createdInstallerSupervisor);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while creating the InstallerSupervisor.");
                return StatusCode(500, "An error occurred while creating the InstallerSupervisor.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<InstallerSupervisor>>> GetInstallerSupervisorsAsync()
        {
            try
            {
                var installerSupervisors = await _installerSupervisorService.GetInstallerSupervisorsAsync();
                return Ok(installerSupervisors);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while fetching the list of InstallerSupervisors.");
                return StatusCode(500, "An error occurred while fetching the list of InstallerSupervisors.");
            }
        }

        [HttpGet("{installerId}/{supervisorId}")]
        public async Task<ActionResult<InstallerSupervisor>> GetInstallerSupervisorAsync(int installerId, int supervisorId)
        {
            try
            {
                var installerSupervisor = await _installerSupervisorService.GetInstallerSupervisorAsync(installerId, supervisorId);
                if (installerSupervisor == null)
                {
                    return NotFound($"InstallerSupervisor with Installer ID {installerId} and Supervisor ID {supervisorId} not found.");
                }

                return Ok(installerSupervisor);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while fetching the InstallerSupervisor.");
                return StatusCode(500, "An error occurred while fetching the InstallerSupervisor.");
            }
        }

        [HttpPut("{installerId}/{supervisorId}")]
        public async Task<IActionResult> UpdateInstallerSupervisorAsync(int installerId, int supervisorId, InstallerSupervisor installerSupervisor)
        {
            try
            {
                if (installerSupervisor.InstallerID != installerId || installerSupervisor.SupervisorID != supervisorId)
                {
                    return BadRequest("Invalid installer-supervisor data or ID mismatch.");
                }

                var updatedInstallerSupervisor = await _installerSupervisorService.UpdateInstallerSupervisorAsync(installerSupervisor);
                if (updatedInstallerSupervisor == null)
                {
                    return NotFound($"InstallerSupervisor with Installer ID {installerId} and Supervisor ID {supervisorId} not found.");
                }

                _logger.Information("InstallerSupervisor updated: {@UpdatedInstallerSupervisor}", updatedInstallerSupervisor);
                return Ok(updatedInstallerSupervisor);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating the InstallerSupervisor.");
                return StatusCode(500, "An error occurred while updating the InstallerSupervisor.");
            }
        }

        [HttpDelete("{installerId}/{supervisorId}")]
        public async Task<IActionResult> DeleteInstallerSupervisorAsync(int installerId, int supervisorId)
        {
            try
            {
                var deleted = await _installerSupervisorService.DeleteInstallerSupervisorAsync(installerId, supervisorId);
                if (!deleted)
                {
                    return NotFound($"InstallerSupervisor with Installer ID {installerId} and Supervisor ID {supervisorId} not found.");
                }

                _logger.Information("InstallerSupervisor deleted: Installer ID {InstallerId}, Supervisor ID {SupervisorId}", installerId, supervisorId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting the InstallerSupervisor.");
                return StatusCode(500, "An error occurred while deleting the InstallerSupervisor.");
            }
        }
    }
}