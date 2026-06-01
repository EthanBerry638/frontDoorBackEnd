
using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Services;
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
    }
}
