using firstDoorBackEnd.Database;
using firstDoorBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace firstDoorBackEnd.Repositories
{
    public class FirstDoorRepository : IFirstDoorRepository
    {
        private readonly FirstDoorContext _context;

        public FirstDoorRepository(FirstDoorContext context)
        {
            _context = context; 
        }

        public async Task<List<SavedJob>> GetAllSavedJobsAsync()
        {
            return await _context.SavedJobs.ToListAsync();
        }
        
        public async Task<SavedJob?> GetJobByIDAsync(int id)
        {
            return await _context.SavedJobs.FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<Status?> UpdateJobStatusAsync(SavedJob jobToUpdate)
        {
            await _context.SaveChangesAsync();
            return jobToUpdate.Status;
        }
    }
}
