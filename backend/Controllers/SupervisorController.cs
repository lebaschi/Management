using Microsoft.AspNetCore.Mvc;
using InstallerManagement.Models;
using InstallerManagement.Services;
using Serilog;
using ILogger = Serilog.ILogger;

namespace InstallerManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupervisorController : ControllerBase
    {
        private readonly ISupervisorService _supervisorService;
        private readonly ILogger _logger;

        public SupervisorController(ISupervisorService supervisorService)
        {
            _supervisorService = supervisorService;
            _logger = Log.ForContext<SupervisorController>();
        }

        [HttpGet]
        public async Task<ActionResult<List<Supervisor>>> GetSupervisorsAsync()
        {
            try
            {
                var supervisors = await _supervisorService.GetSupervisorsAsync();
                return Ok(supervisors);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while fetching supervisors.");
                return StatusCode(500, "An error occurred while fetching supervisors.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Supervisor>> CreateSupervisorAsync(Supervisor supervisor)
        {
            try
            {
                var createdSupervisor = await _supervisorService.CreateSupervisorAsync(supervisor);
                _logger.Information("Supervisor created: {@Supervisor}", createdSupervisor);
                return CreatedAtAction(nameof(GetSupervisorsAsync), createdSupervisor);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while creating the supervisor.");
                return StatusCode(500, "An error occurred while creating the supervisor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupervisorAsync(int id, Supervisor supervisor)
        {
            try
            {
                if (supervisor == null || id != supervisor.SupervisorId)
                {
                    return BadRequest("Invalid supervisor data or ID mismatch.");
                }

                var updatedSupervisor = await _supervisorService.UpdateSupervisorAsync(supervisor);
                if (updatedSupervisor == null)
                {
                    return NotFound($"Supervisor with ID {id} not found.");
                }

                _logger.Information("Supervisor updated: {@UpdatedSupervisor}", updatedSupervisor);
                return Ok(updatedSupervisor);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating the supervisor.");
                return StatusCode(500, "An error occurred while updating the supervisor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupervisorAsync(int id)
        {
            try
            {
                var deleted = await _supervisorService.DeleteSupervisorAsync(id);
                if (!deleted)
                {
                    return NotFound($"Supervisor with ID {id} not found.");
                }

                _logger.Information("Supervisor deleted: ID {SupervisorId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting the supervisor.");
                return StatusCode(500, "An error occurred while deleting the supervisor.");
            }
        }
    }
}