using System;
using Framework.Core.Helpers;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Framework.Data.CacheProviders
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services)
        {
            var redisUri = CommonHelpers.GetValueFromEnv<string>("REDIS_URI");
            var options = ConfigurationOptions.Parse(redisUri);

            if (!options.DefaultDatabase.HasValue)
                throw new ArgumentNullException("DefaultDatabase", "É obrigatório informar o database padrão do Redis.");

            services.AddDistributedRedisCache(config =>
            {
                config.ConfigurationOptions= options;
            });

            //services.AddHealthChecks()
            //    .AddRedis(redisUri, "redis", tags: new string[] { "db", "redis", "cache" });

            return services;
        }
    }
}