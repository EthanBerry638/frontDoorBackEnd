using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Repositories
{
    public interface IFirstDoorRepository
    {
        Task<SavedJob?> GetJobByIDAsync(int id);
    }
}
