
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Services;
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
        public async Task GetJobByIDAsync_ReturnsCorrectJob()
        {
            var job = new SavedJob();
            _mockRepository.Setup(s => s.GetJobByIDAsync(It.IsAny<int>())).ReturnsAsync(job);

            var result = await _service.GetJobByIDAsync(1);

            Assert.That(job, Is.EqualTo(result));
        }
    }
}
