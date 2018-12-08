using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application.GraphQL.Types.Character
{
    public static class CharacterExtensions
    {
        public static IServiceCollection AddGraphQLCharacterModels(this IServiceCollection services)
        {
            services.AddSingleton<CharacterGraphType>();
            services.AddSingleton<CharacterGraphInputType>();

            return services;
        }
    }
}
