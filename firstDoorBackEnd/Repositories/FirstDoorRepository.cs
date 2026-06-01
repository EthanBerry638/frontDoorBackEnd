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
            var expectedJobs = new List<SavedJob>
            {
                new SavedJob { Id = 1, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test"},
                new SavedJob { Id = 2, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test" },
                new SavedJob { Id = 3, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test"}
            };

            return expectedJobs;
        }
    }
}
