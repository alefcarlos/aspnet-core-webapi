﻿using Microsoft.Extensions.DependencyInjection;

namespace Demo.Core.GraphQL.Types.Character
{
    public static class CharacterExtensions
    {
        public static IServiceCollection AddGraphQLCharacterModels(this IServiceCollection services)
        {
            services.AddSingleton<CharacterGraphType>();
            services.AddSingleton<CharacterGraphInputType>();
            services.AddSingleton<CharacterKindEnum>();

            return services;
        }
    }
}
