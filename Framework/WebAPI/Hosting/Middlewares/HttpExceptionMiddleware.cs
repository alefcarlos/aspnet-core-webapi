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
                _logger.LogError(ex, "HttpExceptionMiddleware: Erro inexperado");

                var result = JsonConvert.SerializeObject(new { message = "Infelizmente ocorreu um erro não tratado, entre em contato com os desenolvedores" });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; ;
                await context.Response.WriteAsync(result);
            }
        }
    }
}
