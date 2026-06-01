using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Services
{
    public interface IFirstDoorService
    {
        Task<List<SavedJob>> GetAllSavedJobsAsync();
    }
}
