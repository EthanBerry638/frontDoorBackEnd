using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using firstDoorBackEnd.Models;
using Moq;
using firstDoorBackEnd.Repositories;
using System.Net;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using firstDoorBackEnd.Exceptions;
using firstDoorBackEnd.Services;
using Shouldly;
using FluentAssertions;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;
namespace firstDoorBackEnd.Tests
{
    [TestFixture]
    public class ReedIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;
        private Mock<IReedService> _reedServiceMock;

        [SetUp]
        public void Setup()
        {
            _reedServiceMock = new Mock<IReedService>();
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(Services =>
                {
                    var mockJobs = Services.Where(d => d.ServiceType == typeof(IReedService)).ToList();
                   foreach(var service in mockJobs)
                    {
                        Services.Remove(service);
                    }
                    Services.AddSingleton<IReedService>(_reedServiceMock.Object);
                });
            });
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturn200Ok_AndJobData()
        {
            //Arrange
            _reedServiceMock
                .Setup(x => x
                .GetJobsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<Job> 
                { 
                    new Job(Title: "Junior Developer", EmployerName: "Tech Company", Location: "London", Description: "Entry-level role", Url: "http://example.com/job1")
                });
                    
            //Act
            var response = await _client.GetAsync("/Reed?keyword=developer&location=London");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(json);

            var root = document.RootElement;

            root.ValueKind.Should().Be(JsonValueKind.Array);
            root.GetArrayLength().Should().BeGreaterThan(0);

            var firstItem = root[0];

            firstItem.TryGetProperty("title", out var titleProp).Should().BeTrue();
            titleProp.GetString().Should().Be("Junior Developer");
        }
        [Test]
        public async Task GetAllJobsAsync_ShouldBuildCorrectFilterString()
        {
            // Arrange
            var keyword = "developer";
            var location = "Manchester";

            _reedServiceMock
                .Setup(x => x.GetJobsAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(new List<Job>());

            // Act
            await _client.GetAsync(
                $"/Reed?keyword={keyword}&location={location}");

            // Assert
            var expectedFilter =
                $"title: ({keyword}) AND {keyword} OR \"entry level\" OR \"entry-level\" OR \"trainee\"";

            _reedServiceMock.Verify(
                x => x.GetJobsAsync(expectedFilter, location),
                Times.Once);
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturnEmptyList_WhenNoJobsFound()
        {
            //Arrange
            _reedServiceMock.Setup(x => x.GetJobsAsync(
                It.IsAny<string>(),
                It.IsAny<string>()
                ))
                .ReturnsAsync(new List<Job>());
            //Act
            var response = await _client.GetAsync("/Reed?keyword=qa&location=Leeds");

            //Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<List<Job>>();
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturn500_WhenServiceThrowsAnException()
        {
                       // Arrange
            _reedServiceMock.Setup(x => x.GetJobsAsync(
                It.IsAny<string>(),
                It.IsAny<string>()
                ))
                .ThrowsAsync(new Exception("Service error"));
            // Act
            var response = await _client.GetAsync("/Reed?keyword=qa&location=Leeds");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

    }
}
