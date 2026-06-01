
using firstDoorBackEnd.Controllers;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Services;
using FluentAssertions;
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
        public async Task GetAllSavedJobsAsync_ShouldReturnOkWithEmptyList_WhenServiceReturnsEmptyList()
        {
            var expectedJobs = new List<SavedJob>();

            _serviceMock.Setup(serv => serv.GetAllSavedJobsAsync()).ReturnsAsync(expectedJobs);

            var result = await _controller.GetAllSavedJobsAsync();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value as List<SavedJob>;

            value.Should().BeEquivalentTo(expectedJobs);

            _serviceMock.Verify(serv => serv.GetAllSavedJobsAsync(), Times.Once());
        }
    }
}
