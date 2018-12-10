using Demo.Core.GraphQL.Types.Character;
using Demo.Core.GraphQL.Types.Character.Models;
using Demo.Core.Services.GraphQL;
using GraphQL.Authorization;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Core.GraphQL.Types
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
