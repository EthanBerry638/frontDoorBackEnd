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
            return new List<SavedJob>();
        }
    }
}
