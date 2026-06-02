
using firstDoorBackEnd.Exceptions;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace firstDoorBackEnd.Tests
{
    public class FirstDoorServiceTests
    {
        private Mock<IFirstDoorRepository> _mockRepository;
        private FirstDoorService _service;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IFirstDoorRepository>();
            _service = new FirstDoorService(_mockRepository.Object);
        }

        [Test]
        public async Task GetAllSavedJobsAsync_ShouldReturnEmptyList_WhenRepositoryIsCalledAndReturnsEmptyList()
        {
            var expectedJobs = new List<SavedJob>();

            _mockRepository.Setup(repo => repo.GetAllSavedJobsAsync()).ReturnsAsync(expectedJobs);

            var result = await _service.GetAllSavedJobsAsync();

            result.Should().BeEquivalentTo(expectedJobs);

            _mockRepository.Verify(repo => repo.GetAllSavedJobsAsync(), Times.Once());
        }

        [Test]
        public async Task GetAllSavedJobsAsync_ShouldReturnListOfSavedJobs_WhenRepositoryIsCalledAndReturnsListOfSavedJobs()
        {
            var expectedJobs = new List<SavedJob>
            {
                new SavedJob { Id = 1, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)},
                new SavedJob { Id = 2, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)},
                new SavedJob { Id = 3, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)}
            };

            _mockRepository.Setup(repo => repo.GetAllSavedJobsAsync()).ReturnsAsync(expectedJobs);

            var result = await _service.GetAllSavedJobsAsync();

            result.Should().BeEquivalentTo(expectedJobs);

            _mockRepository.Verify(repo => repo.GetAllSavedJobsAsync(), Times.Once());
        }

        [Test]
        public async Task GetJobByIDAsync_ReturnsCorrectJob()
        {
            var job = new SavedJob { Id = 1, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3) };

            _mockRepository.Setup(r => r.GetJobByIDAsync(1)).ReturnsAsync(job);

            var result = await _service.GetJobByIDAsync(1);

            Assert.That(job, Is.EqualTo(result));
        }

        [Test]
        public async Task GetJobByIDAsync_ReturnsNull_WhenIDDoesNotExist()
        {
            _mockRepository.Setup(r => r.GetJobByIDAsync(It.IsAny<int>())).ReturnsAsync((SavedJob?)null);

            var result = await _service.GetJobByIDAsync(1);

            Assert.IsNull(result);
        }
    }
}
