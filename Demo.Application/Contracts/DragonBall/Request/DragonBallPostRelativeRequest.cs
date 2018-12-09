using Demo.Application.Shared.Enum;

namespace Demo.Application.Contracts.DragonBall.Request
{
    public class DragonBallPostRelativeRequest
    {
        /// <summary>
        /// ID do personagem relativo
        /// </summary>
        public int RelativeId { get; set; }

        /// <summary>
        /// Grau de parentesco do personagem
        /// </summary>
        public ERelativeKind Kind { get; set; }
    }
}
