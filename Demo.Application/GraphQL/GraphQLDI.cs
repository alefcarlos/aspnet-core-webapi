using Demo.Application.GraphQL.Models;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application.GraphQL
{
    public static class GraphQLDI
    {
        public static IServiceCollection AddGraphQLModels(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<DemoQuery>();
            services.AddSingleton<DemoMutation>();
            services.AddSingleton<CharacterType>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new DemoSchema(new FuncDependencyResolver(type => sp.GetService(type))));

            return services;
        }
    }
}
