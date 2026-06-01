using firstDoorBackEnd.Database;
using firstDoorBackEnd.Models;

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
            return new List<SavedJob>();
        }
    }
}
