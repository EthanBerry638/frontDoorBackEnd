using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Services
{
    public interface IFirstDoorService
    {
        Task<List<SavedJob>> GetAllSavedJobsAsync();
        Task<SavedJob?> GetJobByIDAsync(int id);
        Task<Status?> UpdateJobStatusAsync(int id);
    }
}
