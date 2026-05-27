using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Repositories
{
    public interface ICareerJetRepository
    {
        Task<List<Job>> GetAllJobsAsync(string userIp, string userAgent);
    }
}
