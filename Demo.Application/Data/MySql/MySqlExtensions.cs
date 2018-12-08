using Demo.Application.Data.MySql.Repositories;
using Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Demo.Application.Data.MySql
{
    public static class MySqlExtensions
    {
        public static IServiceCollection AddMySql(this IServiceCollection services)
        {
            var connection = CommonHelpers.GetValueFromEnv<string>("MYSQL_CONNECTION");

            services.AddDbContextPool<DbzMySqlContext>(options =>
            {
                options.UseMySql(connection, mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(5, 0), ServerType.MySql);
                    mySqlOptions.MigrationsAssembly("Demo.API");
                });
            });

            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IFamilyRepository, FamilyRepository>();

            return services;
        }
    }
}
