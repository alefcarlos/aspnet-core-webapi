using System.Threading.Tasks;
using Demo.Core.Contracts.Values;
using Demo.Core.ExternalServices.Google;
using Demo.Core.Services;
using Framework.WebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Demo.API.Controllers
{
    /// <summary>
    /// RESTfull services for Caregivers
    /// </summary>
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        private readonly IValuesServices _services;
        private readonly IGoogleMapsAPI _mapsAPI;
        private readonly GoogleApiConfiguration _googleSettings;

        public ValuesController(IValuesServices services, IGoogleMapsAPI mapsAPI, IOptions<GoogleApiConfiguration> googleSettings)
        {
            _services = services;
            _mapsAPI = mapsAPI;
            _googleSettings = googleSettings.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Name = "Alef" });
        }

        [AllowAnonymous]
        [HttpPost("rabbit")]
        public async Task<IActionResult> PostMessage([FromBody]PostMessageRequest request)
        {
            var result = await _services.PostRabbitMessageAsync(request);

            return ParseResult(result);
        }

        [HttpGet("cep")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchCEP(string cep)
        {
            return Ok(await _mapsAPI.SearchAsync(cep, _googleSettings.MapsKey));
        }
    }
}
