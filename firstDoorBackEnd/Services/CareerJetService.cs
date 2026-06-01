using firstDoorBackEnd.Models;
using firstDoorBackEnd.Repositories;

namespace firstDoorBackEnd.Services;

public class CareerJetService : ICareerJetService
{
    private readonly ICareerJetRepository _careerJetRepository;

    public CareerJetService(ICareerJetRepository careerJetRepository)
    {
        _careerJetRepository = careerJetRepository;
    }

    public async Task<List<Job>> GetAllJobsAsync(string userIp, string userAgent)
    {
        var jobs = await _careerJetRepository.GetAllJobsAsync(userIp, userAgent);

        if (!jobs.Any()) return new List<Job>();

        return jobs.Select(j => new Job
        (
            j.Title,
            j.Company,
            j.Locations,
            j.Description,
            j.Url
        )).ToList();
    }
}
