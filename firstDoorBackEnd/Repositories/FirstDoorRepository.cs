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
    }
}
