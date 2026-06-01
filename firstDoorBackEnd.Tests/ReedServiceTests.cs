using firstDoorBackEnd.Controllers;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Services;
using firstDoorBackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using FluentAssertions;
using System.Globalization;

namespace firstDoorBackEnd.Tests;

public class ReedServiceTests
{
    private Mock<IReedRepository> _reedRepositoryMock;
    private ReedService _reedService;

    [SetUp]
    public void Setup()
    {
        _reedRepositoryMock = new Mock<IReedRepository>();
        _reedService = new ReedService(_reedRepositoryMock.Object);
    }

    [Test]
    public async Task GetJobsAsync_CallsRepositoryWithCorrectParameters()
    {
        // Arrange
        _reedRepositoryMock
            .Setup(r => r.GetAllJobsAsync("developer", "London"))
            .ReturnsAsync(new List<Job>());

        // Act
        await _reedService.GetJobsAsync("developer", "London");

        // Assert
        _reedRepositoryMock.Verify(
            r => r.GetAllJobsAsync("developer", "London"),
            Times.Once);
    }

    [Test]
    public async Task GetJobsAsync_ReturnsJobsFromRepository()
    {
        // Arrange
        var expectedJobs = new List<Job>
        {
            new Job("Developer", "Company A", "London", "Job description", "http://example.com/job1"),
            new Job("Developer", "Company B", "London", "Job description", "http://example.com/job2")
        };
        _reedRepositoryMock
            .Setup(r => r.GetAllJobsAsync("developer", "London"))
            .ReturnsAsync(expectedJobs);
        // Act
        var result = await _reedService.GetJobsAsync("developer", "London");
        // Assert
        result.Should().BeEquivalentTo(expectedJobs);
    }
    [Test]
    public async Task GetJobsAsync_ReturnsEmptyListWhenRepositoryReturnsNull()
    {
        _reedRepositoryMock
            .Setup(r => r.GetAllJobsAsync("developer", "London"))
            .ReturnsAsync((List<Job>)null);
        var result = await _reedService.GetJobsAsync("developer", "London");
        result.Should().BeEmpty();
        result.Should().NotBeNull();
    }
}