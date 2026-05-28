using firstDoorBackEnd.Controllers;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using FluentAssertions;

namespace firstDoorBackEnd.Tests;

public class ReedControllerTests
{
    private Mock<IReedService> _reedServiceMock;

    private ReedController _reedController;

    [SetUp]
    public void Setup()
    {
        _reedServiceMock = new Mock<IReedService>();

        _reedController = new ReedController(_reedServiceMock.Object);
    }

    [Test]
    public async Task GetJobsAsync_ShouldReturnOk()
    {
        //Arrange
        
        var testJobs = new List<Job>
            {
            new Job
            (
            "Junior .NET Developer",
            "TechCorp",
            "London",
            "Entry-level backend developer role using C# and ASP.NET",
            "https://example.com/job1"
            ),

            new Job
            (
            "Graduate Software Engineer",
            "FinTech Ltd",
            "Remote",
            "Graduate programme for junior software engineers",
            "https://example.com/job2"
            ),

            new Job
            (
            "Junior Full Stack Developer",
            "StartUp UK",
            "Manchester",
            "Looking for a junior developer with React and .NET skills",
            "https://example.com/job3"
            )
        };
        
        _reedServiceMock
        .Setup(r => r.GetJobsAsync())
        .ReturnsAsync(testJobs);

        //Act

        var result = await _reedController.GetJobsAsync();

        //Assert

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var value = okResult.Value as List<Job>;

        value.Should().NotBeNull();

        value.Should().BeEquivalentTo(testJobs);

        value.Should().HaveCount(3);
        value![0].Title
        .Should().Be("Junior .NET Developer");
    }
}
