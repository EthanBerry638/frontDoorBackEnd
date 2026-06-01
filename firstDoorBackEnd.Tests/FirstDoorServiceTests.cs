
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Services;
using FluentAssertions;
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
        public async Task GetAllSavedJobsAsync_ShouldReturnListOfSavedJobs_WhenRepositoryIsCalledAndReturnsListOfSavedJobs()
        {
            var expectedJobs = new List<SavedJob>
            {
                new SavedJob { Id = 1, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test"},
                new SavedJob { Id = 2, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test" },
                new SavedJob { Id = 3, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test"}
            };

            _mockRepository.Setup(repo => repo.GetAllSavedJobsAsync()).ReturnsAsync(expectedJobs);

            var result = await _service.GetAllSavedJobsAsync();

            result.Should().BeEquivalentTo(expectedJobs, options => options.Excluding(j => j.TimeSaved));

            foreach (var job in result)
            {
                job.TimeSaved.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            }

            _mockRepository.Verify(repo => repo.GetAllSavedJobsAsync(), Times.Once());
        }
    }
}
