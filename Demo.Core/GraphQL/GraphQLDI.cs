﻿using Demo.Core.GraphQL.Types;
using Demo.Core.GraphQL.Types.Character;
using Demo.Core.GraphQL.Types.Family;
using GraphQL;
using GraphQL.Authorization;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Security.Claims;

namespace Demo.Core.GraphQL
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

            services.AddGraphQLAuth();

            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
            }).AddUserContextBuilder(context => new GraphQLUserContext { User = context.User });

            return services;
        }

        public static void AddGraphQLAuth(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
            services.AddTransient<IValidationRule, AuthorizationValidationRule>();

            services.TryAddSingleton(s =>
            {
                var authSettings = new AuthorizationSettings();

                authSettings.AddPolicy("AdminPolicy", _ => _.RequireClaim(ClaimTypes.Role, "admin"));

                return authSettings;
            });
        }

        public static IApplicationBuilder AddGraphQLTypes(this IApplicationBuilder app)
        {
            app.UseGraphQL<ISchema>();

            // use graphql-playground at default url /ui/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });

            return app;
        }
    }
}
