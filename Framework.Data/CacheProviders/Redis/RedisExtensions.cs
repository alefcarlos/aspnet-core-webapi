using System;
using Framework.Core.Helpers;
using Framework.Data.CacheProviders.Redis;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Framework.Data.CacheProviders
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services)
        {
            var redisUri = CommonHelpers.GetValueFromEnv<string>("REDIS_URI");
            var options = ConfigurationOptions.Parse(redisUri);

            if (!options.DefaultDatabase.HasValue)
                throw new ArgumentNullException("DefaultDatabase", "É obrigatório informar o database padrão do Redis.");

            services.AddSingleton(options);

            services.AddSingleton<RedisConnectionWrapper>();
            services.AddScoped<IRedisCacheProvider, RedisCacheProvider>();

            services.AddHealthChecks()
                .AddRedis(redisUri, "redis", tags: new string[] { "db", "redis", "cache" });

            return services;
        }
    }
}