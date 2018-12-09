using Demo.Application.Contracts.SignUp;
using Demo.Application.Services;
using Framework.WebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Demo.API.Controllers.v1
{
    /// <summary>
    /// RESTfull services for SignUp
    /// </summary>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class SingUpController : BaseController
    {
        private readonly ISignUpServices _services;

        public SingUpController(ISignUpServices services)
        {
            _services = services;
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="request">Dados do usuário</param>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        public IActionResult Post([FromBody]SignUpPostRequest request)
        {
            var result = _services.Post(request);

            return result.ParseResult();
        }
    }
}
