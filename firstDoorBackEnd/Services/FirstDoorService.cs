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
            var job = await _firstDoorRepository.GetJobByIDAsync(id);

            if (job is null) return null;

            job.Status = job.Status switch
            {
                Status.To_Apply => Status.Applied,
                Status.Applied => Status.Interviewing,
                Status.Interviewing => Status.Rejected,
                _ => Status.To_Apply
            };

            await _firstDoorRepository.UpdateJobStatusAsync(job);

            return job.Status;
        }
    }
}
