
using firstDoorBackEnd.Controllers;
using firstDoorBackEnd.Services;
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
    }
}
