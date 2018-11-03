using Framework.Services;
using Framework.WebAPI.Responses;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Framework.WebAPI
{
    public static class Helpers
    {
        public static IActionResult ParseResult(this ServicesResult result)
        {
            if (result == null)
                throw new ArgumentException("Informe um result", "result");

            if (result.Success && result.Data == null)
                return new OkResult();

            if (result.Success && result.Data != null)
                return new OkObjectResult(result.Data);

            var reponse = new NotOkDefaultReponse
            {
                Message = result.Error
            };

            return new ObjectResult(reponse)
            {
                StatusCode = (int)result.StatusCode
            };
        }
    }
}
