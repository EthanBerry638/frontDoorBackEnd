using firstDoorBackEnd.Models;
using System.Net.Http;
using System.Text.Json;
namespace firstDoorBackEnd.Services
{
    public class ReedService : IReedService
    {
        private readonly HttpClient _httpClient;
        public ReedService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://www.reed.co.uk/api/1.0/");

            var apiKey = config["ReedApiKey"];

            var authValue = Convert.ToBase64String(
                System.Text.Encoding.ASCII.GetBytes($"{apiKey}:"));

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authValue);
        }

        public async Task<List<Job>> GetJobsAsync(string keyword, string location)
        {
            var response = await _httpClient
                .GetAsync($"search?keywords={Uri.EscapeDataString(keyword)}&locationName={Uri.EscapeDataString(location)}");


            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var json = await JsonDocument.ParseAsync(stream);

            var results = json.RootElement
                .GetProperty("results");

            var jobs = new List<Job>();

            foreach (var item in results.EnumerateArray())
            {
                jobs.Add(new Job(
                    item.GetProperty("jobTitle").GetString() ?? "",
                    item.GetProperty("employerName").GetString() ?? "",
                    item.GetProperty("locationName").GetString() ?? "",
                    item.GetProperty("jobDescription").GetString() ?? "",
                    item.GetProperty("jobUrl").GetString() ?? ""
                ));
            }
            return jobs;
        }
    }
}
