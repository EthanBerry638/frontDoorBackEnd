using System.Net.Http.Headers;
using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Repositories
{
    public class ReedRepository : IReedRepository
    {
        private readonly HttpClient _httpClient;

        public ReedRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress =
                new Uri("https://www.reed.co.uk/api/1.0/");

            string apiKey = "";

            var encodedKey =
                Convert.ToBase64String(
                    System.Text.Encoding.ASCII
                        .GetBytes($"{apiKey}:")
                );

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic",
                    encodedKey
                );
        }

        public async Task<List<Job>> GetAllJobsAsync(
            string keyword,
            string location
        )
        {
            string query =
                $"search?keywords={Uri.EscapeDataString(keyword)}" +
                $"&locationName={Uri.EscapeDataString(location)}";

            HttpResponseMessage response =
                await _httpClient.GetAsync(query);

            if (response.IsSuccessStatusCode)
            {
                var reedResponse =
                    await response.Content
                        .ReadFromJsonAsync<ReedResponse>();

                return reedResponse?.results
                    ?? new List<Job>();
            }

            if (response.StatusCode ==
                System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(
                    "Invalid Reed API request"
                );
            }

            if (response.StatusCode ==
                System.Net.HttpStatusCode.Forbidden)
            {
                throw new Exception(
                    "Invalid Reed API credentials"
                );
            }

            return new List<Job>();
        }
    }
}
