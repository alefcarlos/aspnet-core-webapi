using Demo.Application.GraphQL.Types.Character;
using Demo.Application.Services.GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Application.GraphQL.Types
{
    public class DbzQuery : ObjectGraphType
    {
        public DbzQuery([FromServices]ICharacterGraphServices characterGraphServices)
        {
            Field<CharacterGraphType>(
                "character",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => characterGraphServices.GetByIDAsync(context.GetArgument<int>("id")));

            Field<ListGraphType<CharacterGraphType>>(
                    "characters",
                    resolve: context =>
                    {
                        return characterGraphServices.GetAllAsync();
                    });
        }

    }
}
