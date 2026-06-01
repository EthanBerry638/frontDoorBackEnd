using firstDoorBackEnd.Models;
using firstDoorBackEnd.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Text.Json;

namespace firstDoorBackEnd.Tests
{
    public class FirstDoorIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;
        private Mock<IFirstDoorRepository> _mockRepository;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>();
            _mockRepository = new Mock<IFirstDoorRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
        }

        [Test]
        public async Task GetAllSavedJobsAsyncEndpoint_ShouldReturnOkAndEmptyList_WhenDbIsNotSeeded()
        {
            var client = _factory.CreateClient();
            var expectedJobs = new List<SavedJob>();

            var response = await client.GetAsync("api/FirstDoor");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            var jobs = JsonSerializer.Deserialize<List<SavedJob>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            jobs.Should().BeEquivalentTo(expectedJobs, options => options.Excluding(j => j.TimeSaved));

            foreach (var job in jobs)
            {
                job.TimeSaved.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            }
        }
    }
}
