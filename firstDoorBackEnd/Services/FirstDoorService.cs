using firstDoorBackEnd.Exceptions;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Repositories;

namespace firstDoorBackEnd.Services
{
    public class FirstDoorService(IFirstDoorRepository firstDoorRepository) : IFirstDoorService
    {
        private readonly IFirstDoorRepository _firstDoorRepository = firstDoorRepository;

        public async Task<List<SavedJob>> GetAllSavedJobsAsync()
        {
            return await _firstDoorRepository.GetAllSavedJobsAsync();
        }
        
        public async Task<SavedJob?> GetJobByIDAsync(int id)
        {
            return await _firstDoorRepository.GetJobByIDAsync(id);
        }
    }
}
