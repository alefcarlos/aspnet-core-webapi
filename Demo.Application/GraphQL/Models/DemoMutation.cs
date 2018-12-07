using Demo.Application.Data.MongoDB.Entities;
using Demo.Application.Data.MongoDB.Repositories;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Application.GraphQL.Models
{
    public class DemoMutation : ObjectGraphType
    {
        public DemoMutation([FromServices]ICharacterRepository repository)
        {
            Name = "CreateCharacterMutation";

            Field<CharacterType>(
                "createCharacter",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CharacterType>> { Name = "character" }
                ),
                resolve: context =>
                {
                    var player = context.GetArgument<CharacterEntity>("character");
                    return repository.Create(player);
                });
        }
    }
}
