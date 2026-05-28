using firstDoorBackEnd.Repositories;
using Moq;
using Moq.Protected;
using System.Net.Http.Json;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Exceptions;

namespace firstDoorBackEnd.Tests
{
    public class CareerJetRepositoryTests
    {
        private Mock<IHttpClientFactory> _mockFactory;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private CareerJetRepository _careerJetRepository;

        [SetUp]
        public void SetUp()
        {
            _mockFactory = new Mock<IHttpClientFactory>();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            var client = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://careerjet.com")
            };

            _mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            _careerJetRepository = new CareerJetRepository(client);
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturnListOfJobs_WhenExternalAPIReturnsListOfJobs()
        {
            var mockResponse = new CareerJetResponse
            (
                "JOBS", 1, "1 job found", 1, new List<Job> { new("software engineer", "microsoft", "london", ".NET developer", "test url") }
            );

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = JsonContent.Create(mockResponse)
                });

            var result = await _careerJetRepository.GetAllJobsAsync("129.0.0.1", "Mozilla/5.0");

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturnEmptyListOfJobs_WhenExternalAPIReturnsOkButNoJobs()
        {
            var mockFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = JsonContent.Create(new CareerJetResponse
                    (
                        "JOBS",
                        1,
                        "1 job found",
                        1,
                        new List<Job>()
                    ))
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://careerjet.com")
            };

            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            _careerJetRepository = new CareerJetRepository(client);

            var result = await _careerJetRepository.GetAllJobsAsync("129.0.0.1", "Mozilla/5.0");

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturnEmptyListOfJobs_WhenExternalAPIReturnsOkButJobsAreNull()
        {
            var mockFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = JsonContent.Create(new CareerJetResponse
                    (
                        "JOBS",
                        1,
                        "1 job found",
                        1,
                        null!
                    ))
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://careerjet.com")
            };

            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            _careerJetRepository = new CareerJetRepository(client);

            var result = await _careerJetRepository.GetAllJobsAsync("129.0.0.1", "Mozilla/5.0");

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetAllJobsAsync_ShouldThrowCareerJetBadRequestException_WhenExternalAPIReturnsBadRequest()
        {
            var mockFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = JsonContent.Create(new CareerJetResponse
                    (
                        "JOBS",
                        1,
                        "Unsupported locale code",
                        1,
                        null!
                    ))
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://careerjet.com")
            };

            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            _careerJetRepository = new CareerJetRepository(client);

            var exception = Assert.ThrowsAsync<CareerJetBadRequestException>(async () =>
            {
                await _careerJetRepository.GetAllJobsAsync("129.0.0.1", "Mozilla/5.0");
            });

            Assert.That(exception.Message, Does.Contain("Unsupported locale code"));
        }

        [TestCase("multiple locations found")]
        [TestCase("no matching location found")]
        public async Task GetAllJobsAsync_ShouldReturnEmptyListOfJobs_WhenExternalAPIReturnsOkButTypeIsLocation(string message)
        {
            var mockFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = JsonContent.Create(new CareerJetResponse
                    (
                        "LOCATIONS",
                        1,
                        message,
                        1,
                        null!
                    ))
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://careerjet.com")
            };

            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            _careerJetRepository = new CareerJetRepository(client);

            var result = await _careerJetRepository.GetAllJobsAsync("129.0.0.1", "Mozilla/5.0");

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetAllJobsAsync_ShouldThrowCareerJetForbiddenException_WhenExternalAPIReturnsForbidden()
        {
            var mockFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.Forbidden
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://careerjet.com")
            };

            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            _careerJetRepository = new CareerJetRepository(client);

            var exception = Assert.ThrowsAsync<CareerJetForbiddenException>(async () =>
            {
                await _careerJetRepository.GetAllJobsAsync("129.0.0.1", "Mozilla/5.0");
            });

            Assert.That(exception.Message, Does.Contain("The API key or credentials provided are invalid"));
        }

        [TestCase(System.Net.HttpStatusCode.BadGateway)]
        [TestCase(System.Net.HttpStatusCode.InternalServerError)]
        [TestCase(System.Net.HttpStatusCode.ServiceUnavailable)]
        public async Task GetAllJobsAsync_ShouldReturnEmptyList_WhenExternalAPIReturnsAnOndcoumentedFailureStatusCode(System.Net.HttpStatusCode httpStatusCode)
        {
            var mockFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = httpStatusCode
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://careerjet.com")
            };

            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            _careerJetRepository = new CareerJetRepository(client);

            var result = await _careerJetRepository.GetAllJobsAsync("129.0.0.1", "Mozilla/5.0");

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}
