using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Repositories
{
    public interface ICareerJetRepository
    {
        Task<List<Job>>? GetJobsAsync(string userIp, string userAgent);
    }
}
