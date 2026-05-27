using firstDoorBackEnd.Repositories;
using Moq;
using Moq.Protected;
using System.Net.Http.Json;
using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Tests
{
    public class CareerJetRepositoryTests
    {
        private CareerJetRepository _careerJetRepository;

        [Test]
        public async Task GetAllJobsAsync_ShouldReturnListOfJobs_WhenExternalAPIReturnsListOfJobs()
        {
            var mockFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHanlder = new Mock<HttpMessageHandler>();
            mockHttpMessageHanlder.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = JsonContent.Create(new CareerJetResponse
                    (
                        "JOBS",
                        1,
                        1,
                        new List<Job> { new("software engineer", "microsoft", "london", ".NET developer", "test url") }
                    ))
                });

            var client = new HttpClient(mockHttpMessageHanlder.Object)
            {
                BaseAddress = new Uri("https://careerjet.com")
            };

            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            _careerJetRepository = new CareerJetRepository(client);

            var result = await _careerJetRepository.GetAllJobsAsync("129.0.0.1", "Mozilla/5.0");

            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count());
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
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task GetAllJobsAsync_ShouldReturnEmptyListOfJobs_WhenExternalAPIReturnsOkButJobsIsNull()
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
            Assert.AreEqual(0, result.Count());
        }
    }
}
