using firstDoorBackEnd.Models;
using firstDoorBackEnd.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
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
        }

        [Test]
        public async Task GetAllSavedJobsAsyncEndpoint_ShouldReturnOkAndListOfSavedJobs_WhenDbIsSeeded()
        {
            var expectedJobs = new List<SavedJob>
            {
                new SavedJob { Id = 1, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test"},
                new SavedJob { Id = 2, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test" },
                new SavedJob { Id = 3, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test"}
            };

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    _mockRepository.Setup(repo => repo.GetAllSavedJobsAsync()).ReturnsAsync(expectedJobs);

                    services.AddScoped(_ => _mockRepository.Object);
                });
            }).CreateClient();

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
