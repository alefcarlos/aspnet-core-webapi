using System.Threading.Tasks;
using Demo.Core.Contracts.Values;
using Demo.Core.Services;
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
    [Route("v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        private readonly IValuesServices _services;

        public ValuesController(IValuesServices services)
        {
            _services = services;
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
    }
}
