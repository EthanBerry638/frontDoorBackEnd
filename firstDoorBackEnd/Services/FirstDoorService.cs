using firstDoorBackEnd.Models;
using firstDoorBackEnd.Repositories;

namespace firstDoorBackEnd.Services
{
    public class FirstDoorService : IFirstDoorService
    {
        private readonly IFirstDoorRepository _firstDoorRepository;

        public FirstDoorService(IFirstDoorRepository firstDoorRepository)
        {
            _firstDoorRepository = firstDoorRepository;
        }

        public async Task<SavedJob?> GetJobByIDAsync(int id)
        {
            return await _firstDoorRepository.GetJobByIDAsync(id);
        }
    }
}
