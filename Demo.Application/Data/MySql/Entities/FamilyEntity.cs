using Demo.Application.Contracts.DragonBall.Request;
using Demo.Application.Shared.Enum;
using Framework.Data.EntityFramework;

namespace Demo.Application.Data.MySql.Entities
{
    /// <summary>
    /// Entidade de familiares
    /// </summary>
    public class FamilyEntity : EFEntityBase
    {
        public FamilyEntity()
        {

        }

        public FamilyEntity(DragonBallPostRelativeRequest request)
        {
            RelativeID = request.RelativeId;
            Kind = request.Kind;
        }

        /// <summary>
        /// FK do personagem
        /// </summary>
        public int CharacterID { get; set; }
        public CharacterEntity Character { get; set; }

        /// <summary>
        /// FK do id do familiar
        /// </summary>
        public int RelativeID { get; set; }
        public CharacterEntity Relative { get; set; }

        /// <summary>
        /// Grau de parentesco
        /// </summary>
        public ERelativeKind Kind { get; set; }
    }
}
