using Clinfy.Application.Data.MongoDB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Clinfy.Application.Data.MongoDB
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
