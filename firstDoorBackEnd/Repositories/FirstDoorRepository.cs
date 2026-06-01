using Azure;
using firstDoorBackEnd.Database;
using firstDoorBackEnd.Exceptions;
using firstDoorBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace firstDoorBackEnd.Repositories
{
    public class FirstDoorRepository(FirstDoorContext context) : IFirstDoorRepository
    {
        private readonly FirstDoorContext _context = context;

        public async Task<List<SavedJob>> GetAllSavedJobsAsync()
        {
            return await _context.SavedJobs.ToListAsync();
        }
        
        public async Task<SavedJob?> GetJobByIDAsync(int id)
        {
            return await _context.SavedJobs.FirstOrDefaultAsync(j => j.Id == id) ?? null;
        }
    }
}
