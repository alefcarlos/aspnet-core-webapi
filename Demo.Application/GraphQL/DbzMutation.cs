using Demo.Application.Data.MySql.Entities;
using Demo.Application.Data.MySql.Repositories;
using Demo.Application.GraphQL.Types.Character;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Application.GraphQL.Types
{
    public class DbzMutation : ObjectGraphType
    {
        public DbzMutation([FromServices]ICharacterRepository repository)
        {
            Name = "CreateCharacterMutation";

            Field<CharacterGraphType>(
                "createCharacter",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CharacterGraphInputType>> { Name = "character" }
                ),
                resolve: context =>
                {
                    var player = context.GetArgument<CharacterEntity>("character");
                    return repository.Create(player, true);
                });
        }
    }
}
