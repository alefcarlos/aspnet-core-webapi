using Demo.Core.Data.MongoDB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Core.Data.MongoDB
{
    public static class RepositoriesDI
    {
        public static IServiceCollection AddMongoRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();

            return services;
        }
    }
}
