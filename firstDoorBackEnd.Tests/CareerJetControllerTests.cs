using firstDoorBackEnd.Models;
using firstDoorBackEnd.Controllers;
using firstDoorBackEnd.Services;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace firstDoorBackEnd.Tests;

public class CareerJetControllerTests
{
    private CareerJetController _careerJetController;
    private Mock<ICareerJetService> mockCareerJetService;
    private HttpContext _httpContext;

    [SetUp]
    public void Setup()
    {
        mockCareerJetService = new Mock<ICareerJetService>();
        _careerJetController = new CareerJetController(mockCareerJetService.Object);
        _httpContext = new DefaultHttpContext();
    }

    [Test]
    public async Task GetAllJobsAsync_ReturnsOkResultWithJobs()
    {
        var jobs = new List<Job>()
        {
            new("software engineer", "microsoft", "london", ".NET developer", "test url")
        };

        mockCareerJetService.Setup(s => s.GetAllJobsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(jobs);

        _careerJetController.ControllerContext = new ControllerContext
        {
            HttpContext = _httpContext
        };

        var result = await _careerJetController.GetAllJobsAsync();
        var okResult = result as OkObjectResult;

        Assert.IsInstanceOf<OkObjectResult>(result);
        Assert.That(jobs, Is.EqualTo(okResult!.Value));
    }

    [Test]
    public async Task GetAllJobsAsync_ReturnsOkResultWithEmptyList()
    {
        var jobs = new List<Job>();

        mockCareerJetService.Setup(s => s.GetAllJobsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(jobs);

        _careerJetController.ControllerContext = new ControllerContext
        {
            HttpContext = _httpContext
        };

        var result = await _careerJetController.GetAllJobsAsync();
        var okResult = result as OkObjectResult;

        Assert.IsInstanceOf<OkObjectResult>(result);
        Assert.That(jobs, Is.EqualTo(okResult!.Value));
    }
}