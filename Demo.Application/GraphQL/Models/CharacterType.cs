using Demo.Application.Data.MongoDB.Entities;
using GraphQL.Types;

namespace Demo.Application.GraphQL.Models
{
    /// <summary>
    /// Tipo para a entidade <see cref="CharacterEntity"/>
    /// </summary>
    public class CharacterType : ObjectGraphType<CharacterEntity>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CharacterType()
        {
            Field(x => x.Name);
            Field<StringGraphType>("birthDate", resolve: context => context.Source.BirthDate.ToShortDateString());
        }
    }
}
