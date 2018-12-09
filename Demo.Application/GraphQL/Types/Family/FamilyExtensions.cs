using Microsoft.Extensions.DependencyInjection;

namespace Demo.Core.GraphQL.Types.Family
{
    public static class FamilyExtensions
    {
        public static IServiceCollection AddGraphQLFamilyModels(this IServiceCollection services)
        {
            services.AddSingleton<RelativeGraphType>();
            services.AddSingleton<RelativeKindEnumType>();

            return services;
        }
    }
}
