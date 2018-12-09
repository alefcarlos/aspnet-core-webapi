using GraphQL.Types;

namespace Demo.Application.GraphQL.Types.Character
{
    public class CharacterGraphInputType : InputObjectGraphType
    {
        public CharacterGraphInputType()
        {
            Name = "CharacterInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("birthDate");
            Field<NonNullGraphType<CharacterKindEnum>>("kind");
        }
    }
}
