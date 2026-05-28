using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Services;
using Moq;

namespace firstDoorBackEnd.Tests
{
    public class CareerJetServiceTests
    {
        private CareerJetService _careerJetService;
        private Mock<CareerJetRepository> _mockCareerJetRepository;

        [SetUp]
        public void SetUp()
        {
            _mockCareerJetRepository = new Mock<CareerJetRepository>();
            _careerJetService = new CareerJetService(_mockCareerJetRepository.Object);
        }
    }
}
