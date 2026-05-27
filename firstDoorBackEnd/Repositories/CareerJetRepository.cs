using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Repositories
{
    public class CareerJetRepository : ICareerJetRepository
    {
        private readonly HttpClient _httpClient;

        public CareerJetRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Job>>? GetJobsAsync(string userIp, string userAgent)
        {
            try
            {
                

                var jobs = await _httpClient.GetFromJsonAsync<List<Job>>("search");

                return jobs ?? new List<Job>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
