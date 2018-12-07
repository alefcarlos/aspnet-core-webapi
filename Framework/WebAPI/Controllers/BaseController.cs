using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Framework.WebAPI
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Obtém o ID do usuário logado
        /// </summary>
        protected string GetUserId() => HttpContext.User.Claims.First(c => c.Type == "user_id").Value;

        /// <summary>
        /// Obtém o e-mail do usuário logado
        /// </summary>
        /// <returns></returns>
        protected string GetUserEmail() => HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
    }
}
