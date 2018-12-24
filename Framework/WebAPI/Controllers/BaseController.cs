using Framework.Services;
using Framework.WebAPI.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
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

        // protected IActionResult ParseResult(ServicesResult result)
        // {
        //     if (result == null)
        //         throw new ArgumentException("Informe um result", "result");

        //     if (result.Success && result.Data == null)
        //         return NoContent();

        //     if (result.Success && result.Data != null)
        //         return Ok(result.Data);

        //     var reponse = new NotOkDefaultReponse
        //     {
        //         Message = result.Error
        //     };

        //     return new ObjectResult(reponse)
        //     {
        //         StatusCode = (int)result.StatusCode
        //     };
        // }

        protected IActionResult ParseResult(ServicesResult result, string actionName = "")
        {
            if (result == null)
                throw new ArgumentException("Informe um result", "result");

            if (!result.Success)
            {
                var reponse = new NotOkDefaultReponse
                {
                    Message = result.Error
                };

                return new ObjectResult(reponse)
                {
                    StatusCode = (int)result.StatusCode
                };
            }

            if (result.Data == null)
                return NoContent();

            if (string.IsNullOrWhiteSpace(actionName))
            {
                return new OkObjectResult(result.Data)
                {
                    StatusCode = (int)result.StatusCode
                };
            }

            //Obter o valor da prop ID
            var idProp = result.Data.GetType().GetProperty("ID");
            if (idProp == null)
                return NoContent();

            var idValue = idProp.GetValue(result.Data);

            return CreatedAtAction(actionName, new { id = idValue, version = Request.HttpContext.GetRequestedApiVersion().ToString() }, result.Data);
        }
    }
}
