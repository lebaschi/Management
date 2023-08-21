using Microsoft.EntityFrameworkCore;
using InstallerManagement.Models;
using InstallerManagement.Data;

namespace InstallerManagement.Repositories
{
    public class SupervisorRepository : ISupervisorRepository
    {
        private readonly InstallerManagementDbContext _dbContext;

        public SupervisorRepository(InstallerManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Supervisor>> GetSupervisorsAsync()
        {
            return await _dbContext.Supervisors.ToListAsync();
        }

        public async Task<Supervisor> CreateSupervisorAsync(Supervisor supervisor)
        {
            _dbContext.Supervisors.Add(supervisor);
            await _dbContext.SaveChangesAsync();
            return supervisor;
        }

        public async Task<Supervisor> UpdateSupervisorAsync(Supervisor supervisor)
        {
            _dbContext.Entry(supervisor).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return supervisor;
        }

        public async Task<bool> DeleteSupervisorAsync(int id)
        {
            var supervisor = await _dbContext.Supervisors.FindAsync(id);
            if (supervisor == null)
            {
                return false;
            }

            _dbContext.Supervisors.Remove(supervisor);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}