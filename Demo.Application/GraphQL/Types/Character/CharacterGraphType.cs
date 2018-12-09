using Demo.Application.Data.MySql.Entities;
using Demo.Application.GraphQL.Types.Character.Models;
using Demo.Application.GraphQL.Types.Family;
using Demo.Application.Services;
using GraphQL.Authorization;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Application.GraphQL.Types.Character
{
    /// <summary>
    /// Tipo para a entidade <see cref="CharacterModel"/>
    /// </summary>
    public class CharacterGraphType : ObjectGraphType<CharacterModel>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CharacterGraphType([FromServices]ICharacterGraphServices characterGraphServices)
        {
            Name = "Character";
            Description = "Um personagem do mundo de Dragon Ball Z";

            Field(x => x.ID).Name("id");
            Field(x => x.Name).Description("Nome do personagem");
            Field<CharacterKindEnum>("kind", "Raça do personagem");
            Field(x => x.BirthDate).Description("Ano de nascimento do personagem");
            Field<ListGraphType<RelativeGraphType>>("relatives", resolve: context => characterGraphServices.GetRelativesAsync(context.Source.ID)).Description = "Lista de parentes";
        }
    }
}
