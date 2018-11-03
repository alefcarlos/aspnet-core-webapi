using Framework.WebAPI.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WebAPI.Hosting.Middlewares
{
    /// <summary>
    /// Middleware para captura de exceções.
    /// </summary>
    public class HttpExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// Construtor do middleware.
        /// </summary>
        /// <param name="next">Request Delegate.</param>
        /// <param name="loggerFactory">Fábrica de log.</param>
        public HttpExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<HttpExceptionMiddleware>();
        }

        /// <summary>
        /// Execução da captura.
        /// </summary>
        /// <param name="context">Contexto HTTP.</param>
        /// <returns>Tarefa.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Caught by HttpExceptionMiddleware:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("----- END EXCEPTION -----");

                var log = new
                {
                    IP = context.Connection.RemoteIpAddress != null ? context.Connection.RemoteIpAddress.ToString() : string.Empty,
                    ex.Message,
                    ex.StackTrace
                };

                _logger.LogError("{@log}", log);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var reponseError = new NotOkDefaultReponse
                {
                    Message = "Infelizmente ocorreu um erro não tratado, entre em contato com os desenolvedores"
                };

                var response = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(reponseError));

                await context.Response.Body.WriteAsync(response, 0, response.Length);

                await context.Response.WriteAsync(string.Empty).ConfigureAwait(false);
            }
        }
    }
}
