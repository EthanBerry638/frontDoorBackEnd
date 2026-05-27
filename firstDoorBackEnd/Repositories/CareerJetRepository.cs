using firstDoorBackEnd.Models;
using firstDoorBackEnd.Exceptions;

namespace firstDoorBackEnd.Repositories
{
    public class CareerJetRepository : ICareerJetRepository
    {
        private readonly HttpClient _httpClient;

        public CareerJetRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Job>>? GetAllJobsAsync(string userIp, string userAgent)
        {
            //try
            //{
                string query = $"?keywords={Uri.EscapeDataString("junior software")}" +
                               $"&location={Uri.EscapeDataString("london")}" +
                               $"&user_ip={Uri.EscapeDataString(userIp)}" +
                               $"&user_agent={Uri.EscapeDataString(userAgent)}";

                HttpResponseMessage response = await _httpClient.GetAsync(query);

                if(response.IsSuccessStatusCode)
                {
                    var jobResponse = await response.Content.ReadFromJsonAsync<CareerJetResponse>();
                    return jobResponse?.jobs ?? new List<Job>();
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var errorDetails = await response.Content.ReadFromJsonAsync<CareerJetResponse>();

                    throw new CareerJetBadRequestException(errorDetails?.message!);
                }

                return null;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
        }
    }
}
