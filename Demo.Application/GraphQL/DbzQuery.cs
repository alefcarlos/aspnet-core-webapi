using Demo.Application.Data.MySql.Repositories;
using Demo.Application.GraphQL.Types.Character;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Application.GraphQL.Types
{
    public class DbzQuery : ObjectGraphType
    {
        public DbzQuery([FromServices]ICharacterRepository repository)
        {
            Field<CharacterGraphType>(
                "character",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => repository.Read(context.GetArgument<string>("id")));

            Field<ListGraphType<CharacterGraphType>>(
                "characters",
                resolve: context => repository.Read());
        }

    }
}
