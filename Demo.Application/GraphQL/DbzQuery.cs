using Demo.Core.GraphQL.Types.Character;
using Demo.Core.Services.GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Core.GraphQL.Types
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
