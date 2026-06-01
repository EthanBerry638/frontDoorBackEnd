using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using firstDoorBackEnd.Models;
using Moq;
using firstDoorBackEnd.Repositories;
using System.Net;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using firstDoorBackEnd.Exceptions;

namespace firstDoorBackEnd.Tests
{
    public class CareerJetIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;
        private Mock<ICareerJetRepository> _mockRepository;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>();
            _mockRepository = new Mock<ICareerJetRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
        }

        [Test]
        public async Task GetAllJobsEndpoint_ShouldReturnListOfJobs_WhenRepositoryReturnsListOfJobs()
        {
            var expectedList = new List<CareerJetJob>
            {
                new CareerJetJob{ Title = "test", Company = "test", Locations = "test", Description = "test", Url = "test" },
                new CareerJetJob{ Title = "test", Company = "test", Locations = "test", Description = "test", Url = "test" },
                new CareerJetJob { Title = "test", Company = "test", Locations = "test", Description = "test", Url = "test" }
            };

            var expectedFinalList = new List<Job>
            {
                new Job( "test", "test", "test", "test", "test" ),
                new Job( "test", "test", "test", "test", "test" ),
                new Job ( "test", "test", "test", "test", "test" )
            };

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var mockRepo = new Mock<ICareerJetRepository>();

                    mockRepo.Setup(repo => repo.GetAllJobsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expectedList);

                    services.AddScoped(_ => mockRepo.Object);
                });
            }).CreateClient();

            var response = await client.GetAsync("CareerJet");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var content = await response.Content.ReadAsStringAsync();

            var jobs = JsonSerializer.Deserialize<List<Job>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.That(jobs, Is.EqualTo(expectedFinalList));
        }

        [Test]
        public async Task GetAllJobsEndpoint_ShouldReturnEmptyListOfJobs_WhenRepositoryReturnsEmpty()
        {
            var expectedList = new List<CareerJetJob>();

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var mockRepo = new Mock<ICareerJetRepository>();

                    mockRepo.Setup(repo => repo.GetAllJobsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expectedList);

                    services.AddScoped(_ => mockRepo.Object);
                });
            }).CreateClient();

            var response = await client.GetAsync("CareerJet");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var content = await response.Content.ReadAsStringAsync();

            var jobs = JsonSerializer.Deserialize<List<CareerJetJob>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.That(jobs, Is.EqualTo(expectedList));
        }

        [Test]
        public async Task GetAllJobsEndpoint_ShouldReturnForbidden_WhenRepositoryThrowsForbiddenException()
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var mockRepo = new Mock<ICareerJetRepository>();

                    mockRepo.Setup(repo => repo.GetAllJobsAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new CareerJetForbiddenException("The API key or credentials provided are invalid"));

                    services.AddScoped(_ => mockRepo.Object);
                });
            }).CreateClient();

            var response = await client.GetAsync("CareerJet");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));

            var content = await response.Content.ReadAsStringAsync();

            Assert.That(content.Contains("The API key or credentials provided are invalid"));
        }

        [Test]
        public async Task GetAllJobsEndpoint_ShouldReturnBadRequest_WhenRepositoryThrowsBadRequestException()
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var mockRepo = new Mock<ICareerJetRepository>();

                    mockRepo.Setup(repo => repo.GetAllJobsAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new CareerJetBadRequestException());

                    services.AddScoped(_ => mockRepo.Object);
                });
            }).CreateClient();

            var response = await client.GetAsync("CareerJet");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }
    }
}
