using System.Net.Http;
using System.Threading.Tasks;
using Demo.Core.ExternalServices.Google;
using Refit;
using RichardSzalay.MockHttp;
using Shouldly;
using Xunit;

namespace Demo.Core.Tests.ExternalServices.Google
{
    public class GoogleMapsRepositoryTests
    {
        private readonly MockHttpMessageHandler mockHandler;
        private readonly IGoogleMapsAPI _fixture;

        public GoogleMapsRepositoryTests()
        {
            mockHandler = new MockHttpMessageHandler();

            var settings = new RefitSettings
            {
                HttpMessageHandlerFactory = () => mockHandler
            };

            _fixture = RestService.For<IGoogleMapsAPI>("http://uri", settings);
        }

        [Fact]
        public async Task GetGeoCodeByCEPAsync_ShouldSuccess()
        {
            //Act
            mockHandler.Expect(HttpMethod.Get, "http://uri/json")
               .Respond("application/json", "{status: 'ok'}");

            var result = await _fixture.SearchAsync("09071-425", "key123");

            //Assert
            result.ShouldNotBeNull();
            result.status.ShouldBe("ok");
        }
    }
}
