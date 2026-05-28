using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Services
{
    public interface ICareerJetService
    {
       Task<List<Job>> GetAllJobsAsync(string userIp, string userAgent);
    }
}
