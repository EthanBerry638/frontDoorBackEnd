using firstDoorBackEnd.Controllers;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Services;
using firstDoorBackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using FluentAssertions;

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

}