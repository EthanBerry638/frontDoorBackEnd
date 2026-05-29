using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Services;
using Moq;
using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Tests
{
    public class CareerJetServiceTests
    {
        private CareerJetService _careerJetService;
        private Mock<ICareerJetRepository> _mockCareerJetRepository;

        [SetUp]
        public void SetUp()
        {
            _mockCareerJetRepository = new Mock<ICareerJetRepository>();
            _careerJetService = new CareerJetService(_mockCareerJetRepository.Object);
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturnEmptyList_WhenRepoIsCalledAndReturnsEmptyList()
        {
            var expectedList = new List<Job>();

            _mockCareerJetRepository.Setup(repo => repo.GetAllJobsAsync("test", "test")).ReturnsAsync(expectedList);

            var result = await _careerJetService.GetAllJobsAsync("test", "test");

            Assert.That(result, Is.Empty);

            _mockCareerJetRepository.Verify(repo => repo.GetAllJobsAsync("test", "test"), Times.Once());
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturnListOfJobs_WhenRepoIsCalledAndReturnsListOfJobs()
        {
            var expectedList = new List<Job>
            {
                new("test title 1", "test employer 1", "test location 1", "test description 1", "test url 1"),
                new("test title 2", "test employer 2", "test location 2", "test description 2", "test url 2"),
                new("test title 3", "test employer 3", "test location 3", "test description 3", "test url 3"),
                new("test title 4", "test employer 4", "test location 4", "test description 4", "test url 4"),
                new("test title 5", "test employer 5", "test location 5", "test description 5", "test url 5")
            };

            _mockCareerJetRepository.Setup(repo => repo.GetAllJobsAsync("test", "test")).ReturnsAsync(expectedList);

            var result = await _careerJetService.GetAllJobsAsync("test", "test");

            Assert.That(result.Count, Is.EqualTo(expectedList.Count));

            _mockCareerJetRepository.Verify(repo => repo.GetAllJobsAsync("test", "test"), Times.Once());
        }
    }
}
