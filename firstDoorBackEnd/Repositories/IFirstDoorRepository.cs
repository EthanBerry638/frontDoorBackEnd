using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Repositories
{
    public interface IFirstDoorRepository
    {
        Task<List<SavedJob>> GetAllSavedJobsAsync();
    }
}
