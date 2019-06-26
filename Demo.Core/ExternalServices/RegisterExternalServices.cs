using System;
using System.Net;
using System.Net.Http;
using Demo.Core.ExternalServices.Google;
using Framework.Core.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using Refit;

namespace Demo.Core.ExternalServices
{
    public static class RegisterExternalServices
    {
        /// <summary>
        /// Adiciona todas as dependências de requisições http
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10);

            services.AddRefitClient<IGoogleMapsAPI>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(CommonHelpers.GetValueFromEnv<string>("GOOGLE_GEOCODE_URI")))
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(timeoutPolicy);

            return services;
        }

        /// <summary>
        ///  É adicionado uma política para tentar 3 vezes com uma repetição exponencial, começando em um segundo.
        /// </summary>
        /// <returns></returns>
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.InternalServerError)
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)));
        }
    }
}
