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
            return await _firstDoorRepository.GetAllSavedJobsAsync();
        }
        
        public async Task<SavedJob?> GetJobByIDAsync(int id)
        {
            return await _firstDoorRepository.GetJobByIDAsync(id);
        }

        public async Task<Status?> UpdateJobStatusAsync(int id)
        {
            return null;
        }
    }
}
