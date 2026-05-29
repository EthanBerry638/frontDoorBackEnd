using Microsoft.AspNetCore.Mvc.Testing;

namespace firstDoorBackEnd.Tests
{
    public class CareerJetIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>();
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
        }
    }
}
