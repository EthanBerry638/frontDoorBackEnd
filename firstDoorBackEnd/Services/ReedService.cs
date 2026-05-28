using firstDoorBackEnd.Models;
using System.Net.Http.Headers;
namespace firstDoorBackEnd.Services
{
    public class ReedService : IReedService
    {
        private readonly HttpClient _httpClient;
        public ReedService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://www.reed.co.uk/api/1.0/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"your_api_key:")));
        }
        public async Task<List<Job>> GetJobsAsync(string keyword, string location)
        {
            var response = await _httpClient.GetFromJsonAsync<List<Job>>(
            $"search?keywords={keyword}&location={location}");
            return response ?? new List<Job>();
        }
    }
}
