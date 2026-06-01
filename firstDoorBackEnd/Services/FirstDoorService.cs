using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Services
{
    public class FirstDoorService : IFirstDoorService
    {
        private readonly IFirstDoorRepository _firstDoorRepository;

        public FirstDoorService(IFirstDoorRepository firstDoorRepository)
        {
            _firstDoorRepository = firstDoorRepository;
        }

        public async Task<List<SavedJob>> GetAllSavedJobsAsync()
        {
            var expectedJobs = new List<SavedJob>
            {
                new SavedJob { Id = 1, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test"},
                new SavedJob { Id = 2, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test" },
                new SavedJob { Id = 3, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test"}
            };

            await _firstDoorRepository.GetAllSavedJobsAsync();

            return expectedJobs;
        }
    }
}
