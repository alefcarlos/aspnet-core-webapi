using Demo.Application.GraphQL.Types;
using Demo.Application.GraphQL.Types.Character;
using Demo.Application.GraphQL.Types.Family;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application.GraphQL
{
    public static class GraphQLDI
    {
        public static IServiceCollection AddGraphQLTypes(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<DbzQuery>();
            services.AddSingleton<DbzMutation>();
            services.AddGraphQLCharacterModels();
            services.AddGraphQLFamilyModels();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new DbzSchema(new FuncDependencyResolver(type => sp.GetService(type))));

            return services;
        }
    }
}
