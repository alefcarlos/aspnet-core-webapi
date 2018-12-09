using Demo.Application.Data.MySql.Entities;
using Demo.Application.Shared.Enum;

namespace Demo.Application.Contracts.DragonBall.Request
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
