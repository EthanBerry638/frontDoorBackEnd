using firstDoorBackEnd.Models;
using firstDoorBackEnd.Controllers;
using firstDoorBackEnd.Services;
using Moq;
using NUnit.Framework;
namespace firstDoorBackEnd.Tests;

public class CareerJetControllerTests
{
    private CareerJetController _careerJetController;
    private Mock<ICareerJetService> mockCareerJetService;

    [SetUp]
    public void Setup()
    {
        mockCareerJetService = new Mock<ICareerJetService>();
        _careerJetController = new CareerJetController(mockCareerJetService.Object);
    }

    [Test]
    public async Task GetAllJobsAsync_ReturnsOkResultWithJobs()
    {
        var jobs = new List<Job>()
        {
            new("software engineer", "microsoft", "london", ".NET developer", "test url")
        };

        mockCareerJetService.Setup(s => s.GetAllJobsAsync("string", "string")).ReturnsAsync(jobs);

        var result = await _careerJetController.GetAllJobsAsync("string", "string");
        var okResult = result as Microsoft.AspNetCore.Mvc.OkObjectResult;


        Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
        Assert.That(jobs, Is.EqualTo(okResult!.Value));
    }

    [Test]
    public async Task GetAllJobsAsync_ReturnsOkResultWithEmptyList()
    {
        var jobs = new List<Job>();

        mockCareerJetService.Setup(s => s.GetAllJobsAsync("string", "string")).ReturnsAsync(jobs);

        var result = await _careerJetController.GetAllJobsAsync("string", "string");
        var okResult = result as Microsoft.AspNetCore.Mvc.OkObjectResult;

        Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
        Assert.That(jobs, Is.EqualTo(okResult!.Value));
    }

}