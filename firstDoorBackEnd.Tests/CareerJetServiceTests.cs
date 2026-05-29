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
            var expectedList = new List<CareerJetJob>();

            _mockCareerJetRepository.Setup(repo => repo.GetAllJobsAsync("test", "test")).ReturnsAsync(expectedList);

            var result = await _careerJetService.GetAllJobsAsync("test", "test");

            Assert.That(result, Is.Empty);

            _mockCareerJetRepository.Verify(repo => repo.GetAllJobsAsync("test", "test"), Times.Once());
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturnListOfJobs_WhenRepoIsCalledAndReturnsListOfJobs()
        {
            var expectedList = new List<CareerJetJob>
            {
                new CareerJetJob{ Title = "test", Company = "test", Locations = "test", Description = "test", Url = "test" },
                new CareerJetJob{ Title = "test", Company = "test", Locations = "test", Description = "test", Url = "test" },
                new CareerJetJob { Title = "test", Company = "test", Locations = "test", Description = "test", Url = "test" }
            };

            _mockCareerJetRepository.Setup(repo => repo.GetAllJobsAsync("test", "test")).ReturnsAsync(expectedList);

            var result = await _careerJetService.GetAllJobsAsync("test", "test");

            Assert.That(result.Count, Is.EqualTo(expectedList.Count));

            _mockCareerJetRepository.Verify(repo => repo.GetAllJobsAsync("test", "test"), Times.Once());
        }
    }
}
