
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
            var job = new SavedJob();
            _mockRepository.Setup(s => s.GetJobByIDAsync(It.IsAny<int>())).ReturnsAsync(job);

            var result = await _service.GetJobByIDAsync(1);

            Assert.That(job, Is.EqualTo(result));
        }

        [Test]
        public async Task UpdateJobStatusAsync_ShouldReturnNull_WhenGetByIdAsyncIsCalledAndReturnsNull()
        {
            int id = 300;

            _mockRepository.Setup(repo => repo.GetJobByIDAsync(id)).ReturnsAsync((SavedJob)null!);

            var result = await _service.UpdateJobStatusAsync(id);

            result.Should().BeNull();

            _mockRepository.Verify(repo => repo.GetJobByIDAsync(id), Times.Once());
        }

        [Test]
        public async Task UpdateJobStatusAsync_ShouldIncreaseJobStatusByOne_WhenGetByIdAsyncIsCalledAndReturnsJob()
        {
            int id = 1;
            Status originalStatus = Status.To_Apply;
            Status changedStatus = Status.Applied;
            var job = new SavedJob { Id = id, Status = originalStatus };

            _mockRepository.Setup(repo => repo.GetJobByIDAsync(id)).ReturnsAsync(job);

            var result = await _service.UpdateJobStatusAsync(id);

            result.Should().Be(changedStatus);

            _mockRepository.Verify(repo => repo.GetJobByIDAsync(id), Times.Once());
        }

        [Test]
        public async Task UpdateJobStatusAsync_ShouldWrapAroundBackToZero_WhenGetByIdAsyncIsCalledAndReturnsJobButStatusIsAtTheMaxValue()
        {
            int id = 1;
            Status originalStatus = Status.Rejected;
            Status changedStatus = Status.To_Apply;
            var job = new SavedJob { Id = id, Status = originalStatus };

            _mockRepository.Setup(repo => repo.GetJobByIDAsync(id)).ReturnsAsync(job);

            var result = await _service.UpdateJobStatusAsync(id);

            result.Should().Be(changedStatus);

            _mockRepository.Verify(repo => repo.GetJobByIDAsync(id), Times.Once());
        }

        [Test]
        public async Task UpdateJobStatusAsync_ShouldReturnUpdatedStatus_WhenGetByIdAsyncIsCalledAndReturnsJobAndUpdateIsCalledAndReturnsUpdatedStatus()
        {
            int id = 1;
            Status originalStatus = Status.Applied;
            Status changedStatus = Status.Interviewing;
            var job = new SavedJob { Id = id, Status = originalStatus };

            _mockRepository.Setup(repo => repo.GetJobByIDAsync(id)).ReturnsAsync(job);
            _mockRepository.Setup(repo => repo.UpdateJobStatusAsync(job)).ReturnsAsync(changedStatus);

            var result = await _service.UpdateJobStatusAsync(id);

            result.Should().Be(changedStatus);

            _mockRepository.Verify(repo => repo.GetJobByIDAsync(id), Times.Once());
            _mockRepository.Verify(repo => repo.UpdateJobStatusAsync(job), Times.Once());
        }

        [Test]
        public async Task UpdateJobStatusAsync_ShouldSaveUpdatedStatusToRepository_WhenJobExists()
        {
            int id = 1;
            var job = new SavedJob { Id = id, Status = Status.To_Apply };

            _mockRepository.Setup(repo => repo.GetJobByIDAsync(id)).ReturnsAsync(job);

            await _service.UpdateJobStatusAsync(id);

            _mockRepository.Verify(repo => repo.UpdateJobStatusAsync(It.Is<SavedJob>(j => j.Id == id && j.Status == Status.Applied)), Times.Once());
        }
    }
}
