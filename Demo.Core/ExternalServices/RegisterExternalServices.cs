using System;
using System.Net;
using System.Net.Http;
using Demo.Core.ExternalServices.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleApiConfiguration>(configuration.GetSection(nameof(GoogleApiConfiguration)));
            var configs = services.BuildServiceProvider().GetRequiredService<IOptions<GoogleApiConfiguration>>().Value;

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10);

            services.AddRefitClient<IGoogleMapsAPI>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.GeoCodeURI))
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
