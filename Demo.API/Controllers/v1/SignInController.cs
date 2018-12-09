using Demo.Application.Contracts.SignIn;
using Demo.Application.Services;
using Framework.WebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Demo.API.Controllers.v1
{
    /// <summary>
    /// RESTfull services for SignIn
    /// </summary>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class SignInController : BaseController
    {
        private readonly ISignInServices _services;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="services">Dependência do serviço</param>
        public SignInController(ISignInServices services)
        {
            _services = services;
        }

        /// <summary>
        /// Tenta logar um usuário a partir de e-mail e senha
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("token")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        public IActionResult Post([FromBody]SignInPostRequest request)
        {
            var result = _services.GenerateToken(request);

            return result.ParseResult();
        }

    }
}
