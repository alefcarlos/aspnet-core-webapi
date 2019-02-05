using System.Threading.Tasks;
using Bogus;
using Demo.Core.Data.ApiClient.Google;
using Demo.Core.Data.ApiClient.Google.Views;
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

        static void FakerForSuccess(string cep)
        {
            // var latLongFaker = new Faker<Location>()
            //     .RuleFor(l => l.lat, f => f.System.Radom. );

            // var expectedResult = new Faker<GoogleGeoCodeView>()
            //     .RuleFor(x => x.status, x => "ok")
            //     .RuleFor();
        }

        [Theory]
        [InlineData("79086-160")]
        [InlineData("65026-040")]
        public async Task GetGeoCodeByCEPAsync_ShouldSuccess(string cep)
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();

            // var expectedResult = new Faker<GoogleGeoCodeView>()
            //     .RuleFor(x => x.status, x => "ok")
            //     .RuleFor()
            // {
            //     status = "ok",
            //     results = new Result[] {
            //         new Result{
            //               geometry = new Geometry{
            //                    location = new Location{

            //                    }
            //               }
            //         }
            //     }
            // };

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