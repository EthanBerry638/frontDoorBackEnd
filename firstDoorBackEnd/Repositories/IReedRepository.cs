using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Repositories
{
    public interface IReedRepository
    {

        Task<List<Job>> GetAllJobsAsync(
        string keyword,
        string location
        );
    }
}
