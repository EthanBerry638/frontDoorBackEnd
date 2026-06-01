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
        public async Task GetJobByIDAsync_ReturnsCorrectJobWithStatus200()
        {
            var job = new SavedJob();
            _serviceMock.Setup(s => s.GetJobByIDAsync(It.IsAny<int>())).ReturnsAsync(job);

            var result = await _controller.GetJobByIDAsync(1);
            var okResult = result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.That(job, Is.EqualTo(okResult!.Value));
        }

    }
}
