using firstDoorBackEnd.Models;
namespace firstDoorBackEnd.Services

{
    public interface IReedService
    {
       public Task<List<Job>> GetJobsAsync();
    }
}
