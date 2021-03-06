﻿using Microsoft.Extensions.DependencyInjection;

namespace Demo.Core.Services.GraphQL
{
    public static class GraphServicesDI
    {
        public static IServiceCollection AddGraphServices(this IServiceCollection services)
        {
            services.AddSingleton<ICharacterGraphServices, CharacterGraphServices>();

            return services;
        }
    }
}
