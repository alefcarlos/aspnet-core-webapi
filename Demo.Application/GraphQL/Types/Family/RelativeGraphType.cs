using Demo.Core.GraphQL.Types.Character;
using Demo.Core.GraphQL.Types.Family.Models;
using Demo.Core.Services.GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Core.GraphQL.Types.Family
{
    /// <summary>
    /// Tipo para a entidade <see cref="RelativeModel"/>
    /// </summary>
    public class RelativeGraphType : ObjectGraphType<RelativeModel>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RelativeGraphType([FromServices]ICharacterGraphServices characterGraphServices)
        {
            Name = "Relative";
            Description = "Informações de parentesco de um personagem do Dragon Ball";

            Field(x => x.ID).Name("id");
            Field(x => x.Name).Description("Nome do personagem");
            Field<CharacterKindEnum>("kind", "Raça do personagem");
            Field(x => x.BirthDate).Description("Ano de nascimento do personagem");
            Field<RelativeKindEnumType>("relativeKind", "Grau de parentesco");
            Field<ListGraphType<RelativeGraphType>>("relatives", resolve: context => characterGraphServices.GetRelativesAsync(context.Source.ID)).Description = "Lista de parentes";
        }
    }
}
