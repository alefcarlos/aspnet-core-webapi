using System.Threading.Tasks;
using Demo.Core.Contracts.Values;
using Demo.Core.ExternalServices.Google;
using Demo.Core.Services;
using Framework.Core.Helpers;
using Framework.WebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public ValuesController(IValuesServices services, IGoogleMapsAPI mapsAPI)
        {
            _services = services;
            _mapsAPI = mapsAPI;
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
            return Ok(await _mapsAPI.SearchAsync(cep, CommonHelpers.GetValueFromEnv<string>("GOOGLE_MAPS_KEY")));
        }
    }
}
