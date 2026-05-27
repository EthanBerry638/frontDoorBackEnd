using firstDoorBackEnd.Repositories;
using Moq;
using Moq.Protected;

namespace firstDoorBackEnd.Tests
{
    public class CareerJetRepositoryTests
    {
        private CareerJetRepository _careerJetRepository;

        [Test]
        public async Task GetJobsAsync_ShouldReturnListOfJobs_WhenExternalAPIReturnsListOfJobs()
        {
            var mockFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHanlder = new Mock<HttpMessageHandler>();
            mockHttpMessageHanlder.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK
                });

            var client = new HttpClient(mockHttpMessageHanlder.Object);
        }
    }
}
