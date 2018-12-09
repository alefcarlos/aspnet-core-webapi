using Demo.Core.Data.MySql.Entities;
using Demo.Core.Shared.Enum;

namespace Demo.Core.Contracts.DragonBall.Request
{
    public class DragonBallPostRequest
    {
        /// <summary>
        /// Nome do personagem
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Informação de nascimento
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// Raça do personagem
        /// </summary>
        public ECharecterKind Kind { get; set; }
    }
}
