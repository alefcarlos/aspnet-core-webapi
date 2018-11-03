using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// RESTfull services for Caregivers
    /// </summary>
    [Authorize("Bearer")]
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Name = "Alef" });
        }
    }
}
