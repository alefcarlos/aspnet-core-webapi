using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Data.MySql
{
    public static class MySqlExtensions
    {
        public static IServiceCollection AddMySqlHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddMySql(configuration.GetConnectionString("MYSQL"), "mysql", tags: new string[] { "db", "mysql" });

            return services;
        }
    }
}
