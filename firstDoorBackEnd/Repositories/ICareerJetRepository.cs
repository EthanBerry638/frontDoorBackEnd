using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Repositories
{
    public interface ICareerJetRepository
    {
        Task<List<CareerJetJob>> GetAllJobsAsync(string userIp, string userAgent);
    }
}
