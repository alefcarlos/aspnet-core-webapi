using Framework.Data.MongoDB;
using System;

namespace Demo.Application.Data.MongoDB.Entities
{
    /// <summary>
    /// Entidade de personagem
    /// </summary>
    public class CharacterEntity : MongoEntityBase
    {
        /// <summary>
        /// Nome do personagem
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}
