using Framework.Data.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Application.Data.MySql.Entities
{
    /// <summary>
    /// Entidade de familiares
    /// </summary>
    public class FamilyEntity : EFEntityBase
    {
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
        public EFamilyKind Kind { get; set; }
    }

    /// <summary>
    /// Enumerador de grau de parentesco
    /// </summary>
    public enum EFamilyKind
    {
        Brother = 1,
        Sister,
        Son,
        Daugther,
        Spouse,
        Father,
        Mother
    }
}
