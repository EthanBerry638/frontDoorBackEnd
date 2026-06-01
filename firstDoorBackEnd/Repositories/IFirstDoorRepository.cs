using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Repositories
{
    public interface IFirstDoorRepository
    {
        Task<List<SavedJob>> GetAllSavedJobsAsync();
        Task<SavedJob?> GetJobByIDAsync(int id);
        Task<Status?> UpdateJobStatus(int id);
    }
}
