using firstDoorBackEnd.Models;
using System.Net.Http.Headers;
using firstDoorBackEnd.Repositories;
namespace firstDoorBackEnd.Services

{
    public class ReedService : IReedService
    {
        private readonly IReedRepository _reedRepository;

        public ReedService(IReedRepository reedRepository)
        {
            _reedRepository = reedRepository;
        }

        public async Task<List<Job>> GetJobsAsync(string keyword, string location)
        {
            return await Task.FromResult(new List<Job>());
        }
    }

}