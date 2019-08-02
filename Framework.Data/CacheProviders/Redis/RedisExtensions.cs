using System;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace Framework.Data.CacheProviders
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redisUri = configuration.GetConnectionString("Redis");
            var options = ConfigurationOptions.Parse(redisUri);

            if (!options.DefaultDatabase.HasValue)
                throw new ArgumentNullException("DefaultDatabase", "É obrigatório informar o database padrão do Redis.");

            services.AddStackExchangeRedisCache(config =>
            {
                config.ConfigurationOptions = options;
            });

            services.AddHealthChecks()
               .AddRedis(redisUri, "redis", tags: new string[] { "db", "redis", "cache" });

            return services;
        }
    }
}