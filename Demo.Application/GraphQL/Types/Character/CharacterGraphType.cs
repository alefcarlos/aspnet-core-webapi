using Demo.Application.Data.MySql.Entities;
using GraphQL.Types;

namespace Demo.Application.GraphQL.Types.Character
{
    /// <summary>
    /// Tipo para a entidade <see cref="CharacterEntity"/>
    /// </summary>
    public class CharacterGraphType : ObjectGraphType<CharacterEntity>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CharacterGraphType()
        {
            Name = "Character";
            Description = "Um personagem do mundo de Dragon Ball Z";

            Field(x => x.ID).Name("id");
            Field(x => x.Name).Description("Nome do personagem");
            Field<CharacterKindEnum>("kind", "Raça do personagem");

            Field<StringGraphType>("birthDate", resolve: context => context.Source.BirthDate.ToShortDateString());
        }
    }
}
