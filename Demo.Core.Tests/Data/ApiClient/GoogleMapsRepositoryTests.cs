using System.Threading.Tasks;
using Demo.Core.Data.ApiClient.Google;
using RichardSzalay.MockHttp;
using Shouldly;
using Xunit;

namespace Demo.Core.Tests.Data.ApiClient
{
    public class GoogleMapsRepositoryTests
    {

        static GoogleApiConfiguration GetMapsConfig() => new GoogleApiConfiguration
        {
            MapsKey = "AIzaSyD6DxZlq_qWFSYs640jw9k_IvFltjM-Uew",
            GeoCodeURI = "https://maps.googleapis.com/maps/api/geocode"
        };

        [Theory]
        [InlineData("09071-483")]
        [InlineData("09071-483")]
        public async Task GetGeoCodeByCEPAsync_ShouldSuccess(string cep)
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("https://maps.googleapis.com/maps/api/geocode/*")
                    .Respond("application/json", "{'status' : 'ok'}"); // Respond with JSON

            // Inject the handler or client into your application code
            var client = mockHttp.ToHttpClient();

            var subjectUnderTest = new GoogleMapsRepository(client, GetMapsConfig());

            //Act
            var result = await subjectUnderTest.GetGeoCodeByCEPAsync(cep);

            //Assert
            result.ShouldNotBeNull();
            result.status.ShouldBe("ok");
        }
    }
}