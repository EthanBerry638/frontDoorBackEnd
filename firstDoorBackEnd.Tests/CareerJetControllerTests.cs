using firstDoorBackEnd.Models;
using firstDoorBackEnd.Controllers;
using firstDoorBackEnd.Services;
using Moq;
using NUnit.Framework;
namespace firstDoorBackEnd.Tests;

public class CareerJetControllerTests
{
    private CareerJetController _careerJetController;

    [Test]
    public async Task GetAllJobsAsync_CallsGetAllJobsAsyncFromService()
    {
        var mockCareerJetService = new Mock<ICareerJetService>();
        var jobs = new List<Job>();

        mockCareerJetService.Setup(s => s.GetAllJobsAsync("string","string")).ReturnsAsync(jobs);

        var controller = new CareerJetController(mockCareerJetService.Object);

        var result = await controller.GetAllJobsAsync("string","string");

        mockCareerJetService.Verify(s => s.GetAllJobsAsync("string", "string"), Times.Once);
    }
}
