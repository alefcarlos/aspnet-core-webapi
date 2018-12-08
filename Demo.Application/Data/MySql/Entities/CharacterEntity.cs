using Framework.Data.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Application.Data.MySql.Entities
{
    /// <summary>
    /// Entidade de personagem
    /// </summary>
    public class CharacterEntity : EFEntityBase
    {
        /// <summary>
        /// ID do registro
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// Nome do personagem
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Especie
        /// </summary>
        public ECharecterKind Kind { get; set; }
    }

    /// <summary>
    /// Enumerador de especies
    /// </summary>
    public enum ECharecterKind
    {
        /// <summary>
        /// Humano
        /// </summary>
        Human = 1,

        /// <summary>
        /// Sayajin
        /// </summary>
        Sayajin
    }
}
