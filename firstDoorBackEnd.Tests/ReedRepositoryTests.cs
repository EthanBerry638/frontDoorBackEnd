using firstDoorBackEnd.Repositories;
using Moq;
using Moq.Protected;
using System.Net.Http.Json;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Exceptions;

namespace firstDoorBackEnd.Tests
{
    public class ReedRepositoryTests
    {
        private Mock<IHttpClientFactory> _mockFactory;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private ReedRepository _reedRepository;

        [SetUp]
        public void SetUp()
        {
            _mockFactory = new Mock<IHttpClientFactory>();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            var client = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://reed.co.uk")
            };

            _mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            _reedRepository = new ReedRepository(client);
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturnListOfJobs_WhenExternalAPIReturnsListOfJobs()
        {
            var mockResponse = new ReedResponseDto
            {
                results = new List<ReedJobDto>
                {
                    new ReedJobDto
                    {
                        jobTitle = "test",
                        employerName = "test",
                        locationName = "test",
                        jobDescription = "test",
                        jobUrl = "test"
                    }
                }
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = JsonContent.Create(mockResponse)
                });

            var result = await _reedRepository.GetAllJobsAsync(
                "developer", 
                "London");

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
        }


        [Test]
        public async Task GetAllJobsAsync_ShouldReturnEmptyListOfJobs_WhenExternalAPIReturnsOkButNoJobs()
        {
            var mockResponse = new ReedResponseDto
            {
                results = new List<ReedJobDto>()
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = JsonContent.Create(mockResponse)
                });

            var result = await _reedRepository.GetAllJobsAsync(
                "developer", 
                "London");

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturnEmptyListOfJobs_WhenExternalAPIReturnsOkButJobsAreNull()
        {
            var mockResponse = new ReedResponseDto
            {
                results = null!
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = JsonContent.Create(mockResponse)
                });

            var result = await _reedRepository.GetAllJobsAsync(
                "developer", 
                "London");

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetAllJobsAsync_ShouldThrowCareerJetBadRequestException_WhenExternalAPIReturnsBadRequest()
        {
            var mockResponse = new ReedResponseDto
            {
                results = null!
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = JsonContent.Create(mockResponse)
                });

            var exception = Assert.ThrowsAsync<Exception>(async () =>
            {
                await _reedRepository.GetAllJobsAsync(
                "developer", 
                "London");
            });

            Assert.That(exception.Message, Does.Contain("Invalid Reed API request"));
        }

        [TestCase("multiple locations found")]
        [TestCase("no matching location found")]
        public async Task GetAllJobsAsync_ShouldReturnEmptyListOfJobs_WhenExternalAPIReturnsOkButTypeIsLocation(string message)
        {
            var mockResponse = new ReedResponseDto
            {
                results = null!
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = JsonContent.Create(mockResponse)
                });

            var result = await _reedRepository.GetAllJobsAsync(
                "developer", 
                "London");

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetAllJobsAsync_ShouldThrowCareerJetForbiddenException_WhenExternalAPIReturnsForbidden()
        {
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.Forbidden
                });

            var exception = Assert.ThrowsAsync<Exception>(async () =>
            {
                await _reedRepository.GetAllJobsAsync(
                "developer", 
                "London");
            });

            Assert.That(exception.Message, Does.Contain("Invalid Reed API credentials"));
        }

        [TestCase(System.Net.HttpStatusCode.BadGateway)]
        [TestCase(System.Net.HttpStatusCode.InternalServerError)]
        [TestCase(System.Net.HttpStatusCode.ServiceUnavailable)]
        public async Task GetAllJobsAsync_ShouldReturnEmptyList_WhenExternalAPIReturnsAnOndcoumentedFailureStatusCode(System.Net.HttpStatusCode httpStatusCode)
        {
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = httpStatusCode
                });

            var result = await _reedRepository.GetAllJobsAsync(
                "developer", 
                "London");

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}
