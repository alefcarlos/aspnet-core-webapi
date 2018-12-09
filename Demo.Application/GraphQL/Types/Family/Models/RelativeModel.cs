using Demo.Application.Data.MySql.Entities;
using Demo.Application.GraphQL.Types.Character.Models;

namespace Demo.Application.GraphQL.Types.Family.Models
{
    public class RelativeModel : CharacterModel
    {
        /// <summary>
        /// Grau de parentesco
        /// </summary>
        public EFamilyKind RelativeKind { get; set; }

        public RelativeModel()
        {

        }

        public RelativeModel(FamilyEntity entity)
        {
            ID = entity.Relative.ID;
            Name = entity.Relative.Name;
            BirthDate = entity.Relative.BirthDate;
            Kind = entity.Relative.Kind;
            RelativeKind = entity.Kind;
        }
    }
}
