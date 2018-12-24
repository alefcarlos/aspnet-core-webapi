using Demo.Core.Contracts.SignUp;
using Demo.Core.Services;
using Framework.WebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Demo.API.Controllers.v1
{
    /// <summary>
    /// RESTfull services for SignUp
    /// </summary>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult Post([FromBody]SignUpPostRequest request)
        {
            var result = _services.Post(request);

            return ParseResult(result);
        }
    }
}
