
using firstDoorBackEnd.Controllers;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace firstDoorBackEnd.Tests
{
    public class FirstDoorControllerTests
    {
        private Mock<IFirstDoorService> _serviceMock;
        private FirstDoorController _controller;

        [SetUp]
        public void SetUp()
        {
            _serviceMock = new Mock<IFirstDoorService>();
            _controller = new FirstDoorController(_serviceMock.Object);
        }

        [Test]
        public async Task GetAllSavedJobsAsync_ShouldReturnOkWithEmptyList_WhenServiceReturnsEmptyList()
        {
            var expectedJobs = new List<SavedJob>();

            _serviceMock.Setup(serv => serv.GetAllSavedJobsAsync()).ReturnsAsync(expectedJobs);

            var result = await _controller.GetAllSavedJobsAsync();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value as List<SavedJob>;

            value.Should().BeEquivalentTo(expectedJobs);

            _serviceMock.Verify(serv => serv.GetAllSavedJobsAsync(), Times.Once());
        }

        [Test]
        public async Task GetAllSavedJobsAsync_ShouldReturnOkWithListOfSavedJobs_WhenServiceReturnsListOfSavedJobs()
        {
            var expectedJobs = new List<SavedJob>
            {
                new SavedJob { Id = 1, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test"},
                new SavedJob { Id = 2, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test" },
                new SavedJob { Id = 3, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test"}
            };

            _serviceMock.Setup(serv => serv.GetAllSavedJobsAsync()).ReturnsAsync(expectedJobs);

            var result = await _controller.GetAllSavedJobsAsync();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value as List<SavedJob>;

            value.Should().BeEquivalentTo(expectedJobs, options => options.Excluding(j => j.TimeSaved));

            foreach (var job in value)
            {
                job.TimeSaved.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            }

            _serviceMock.Verify(serv => serv.GetAllSavedJobsAsync(), Times.Once());
        }
    }
}
