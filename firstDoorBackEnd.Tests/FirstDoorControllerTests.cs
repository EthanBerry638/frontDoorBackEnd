
using FluentAssertions;
using firstDoorBackEnd.Controllers;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Services;
using Microsoft.AspNetCore.Http.HttpResults;
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
            var jobs = okResult.Value as List<SavedJob>;

            jobs.Should().BeEquivalentTo(expectedJobs);

            _serviceMock.Verify(serv => serv.GetAllSavedJobsAsync(), Times.Once());
        }

        [Test]
        public async Task GetAllSavedJobsAsync_ShouldReturnOkWithListOfSavedJobs_WhenServiceReturnsListOfSavedJobs()
        {
            var expectedJobs = new List<SavedJob>
            {
                new SavedJob { Id = 1, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)},
                new SavedJob { Id = 2, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)},
                new SavedJob { Id = 3, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)}
            };

            _serviceMock.Setup(serv => serv.GetAllSavedJobsAsync()).ReturnsAsync(expectedJobs);

            var result = await _controller.GetAllSavedJobsAsync();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var jobs = okResult.Value as List<SavedJob>;

            jobs.Should().BeEquivalentTo(expectedJobs);

            _serviceMock.Verify(serv => serv.GetAllSavedJobsAsync(), Times.Once());
        }


        [Test]
        public async Task GetJobByIDAsync_ReturnsCorrectJobWithStatus200()
        {
            var job = new SavedJob();
            _serviceMock.Setup(s => s.GetJobByIDAsync(It.IsAny<int>())).ReturnsAsync(job);

            var result = await _controller.GetJobByIDAsync(1);
            var okResult = result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.That(job, Is.EqualTo(okResult!.Value));
        }


        [Test]
        public async Task GetJobByIDAsync_Returns404_WhenIDDoesNotExist()
        {
            var job = new SavedJob();
            _serviceMock.Setup(s => s.GetJobByIDAsync(It.IsAny<int>())).ReturnsAsync((SavedJob?)null);

            var result = await _controller.GetJobByIDAsync(1);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
