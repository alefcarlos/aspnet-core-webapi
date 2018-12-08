using Demo.Application.GraphQL.Types.Character;
using Demo.Application.GraphQL.Types.Character.Models;
using Demo.Application.Services;
using GraphQL.Authorization;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Application.GraphQL.Types
{
    public class DbzMutation : ObjectGraphType
    {
        public DbzMutation([FromServices]ICharacterGraphServices characterGraphServices)
        {
            this.AuthorizeWith("AdminPolicy");
            Name = "CreateCharacterMutation";

            Field<CharacterGraphType>(
                "createCharacter",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CharacterGraphInputType>> { Name = "character" }
                ),
                resolve: context =>
                {
                    var character = context.GetArgument<CharacterModel>("character");
                    return characterGraphServices.CreateAsync(character);
                });
        }
    }
}
