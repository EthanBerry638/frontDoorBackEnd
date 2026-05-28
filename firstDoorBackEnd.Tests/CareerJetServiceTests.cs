using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Services;
using Moq;
using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Tests
{
    public class CareerJetServiceTests
    {
        private CareerJetService _careerJetService;
        private Mock<HttpClient> _mockHttpClient;
        private Mock<CareerJetRepository> _mockCareerJetRepository;

        [SetUp]
        public void SetUp()
        {
            _mockHttpClient = new Mock<HttpClient>();
            _mockCareerJetRepository = new Mock<CareerJetRepository>(_mockHttpClient.Object);
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
    }
}
