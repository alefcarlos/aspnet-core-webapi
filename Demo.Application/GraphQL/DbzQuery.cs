using Demo.Application.GraphQL.Types.Character;
using Demo.Application.Services;
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
                resolve: context => characterGraphServices.GetByID(context.GetArgument<int>("id")));

            Field<ListGraphType<CharacterGraphType>>(
                    "characters",
                    resolve: context => characterGraphServices.GetAll());
        }

    }
}
