using Microsoft.Extensions.DependencyInjection;

namespace Framework.Data.MySql
{
    public static class MySqlExtensions
    {
        public static IServiceCollection AddMySqlHealthCheck(this IServiceCollection services, string connectionString)
        {
            services.AddHealthChecks()
                .AddMySql(connectionString, "mysql", tags: new string[] { "db", "mysql" });

            return services;
        }
    }
}
