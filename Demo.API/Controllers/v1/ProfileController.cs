using Demo.Core.Services;
using Framework.WebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.API.Controllers.v1
{
    /// <summary>
    /// RESTfull services for User profile
    /// </summary>
    [Authorize("Bearer")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {
        private readonly IProfileServices _services;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="services"></param>
        public ProfileController(IProfileServices services)
        {
            _services = services;
        }

        [HttpPost("photo")]
        public async Task<IActionResult> Photo([FromBody] byte[] image)
        {
            var result = await _services.SaveUserPhotoAsync(GetUserId(), image);
            return ParseResult(result);
        }
    }
}
