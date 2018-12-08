using Demo.Application.Data.MySql.Entities;
using GraphQL.Types;

namespace Demo.Application.GraphQL.Types.Character
{
    public class CharacterKindEnum : EnumerationGraphType<ECharecterKind>
    {
        public CharacterKindEnum()
        {
            Name = "Kind";
            Description = "Raça do personagem";
        }
    }
}
