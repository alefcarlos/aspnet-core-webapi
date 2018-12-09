using Demo.Core.Data.MySql.Entities;
using Demo.Core.Shared.Enum;
using GraphQL.Types;

namespace Demo.Core.GraphQL.Types.Character
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
