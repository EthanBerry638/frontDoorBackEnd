using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Services
{
    public interface IFirstDoorService
    {
        Task<SavedJob?> GetJobByIDAsync(int id);
    }
}
