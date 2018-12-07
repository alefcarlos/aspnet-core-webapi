using Demo.Application.Data.MongoDB.Repositories;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Application.GraphQL.Models
{
    public class DemoQuery : ObjectGraphType
    {
        public DemoQuery([FromServices]ICharacterRepository repository)
        {
            Field<CharacterType>(
                "player",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => repository.Read(context.GetArgument<string>("id")));

            //Field<ListGraphType<CharacterType>>(
            //    "players",
            //    resolve: context => repository.Read();
        }

    }
}
