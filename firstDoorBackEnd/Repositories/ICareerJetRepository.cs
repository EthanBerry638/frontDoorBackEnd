using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Repositories
{
    public interface ICareerJetRepository
    {
        Task<List<Job>>? GetJobs();
    }
}
