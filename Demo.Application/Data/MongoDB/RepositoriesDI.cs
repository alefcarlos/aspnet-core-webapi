using Demo.Application.Data.MongoDB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application.Data.MongoDB
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
