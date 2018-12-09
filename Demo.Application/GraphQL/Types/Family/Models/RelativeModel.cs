using Demo.Core.Data.MySql.Entities;
using Demo.Core.GraphQL.Types.Character.Models;
using Demo.Core.Shared.Enum;

namespace Demo.Core.GraphQL.Types.Family.Models
{
    public class RelativeModel : CharacterModel
    {
        /// <summary>
        /// Grau de parentesco
        /// </summary>
        public ERelativeKind RelativeKind { get; set; }

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
