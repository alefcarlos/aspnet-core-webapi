using Demo.Core.Contracts.DragonBall.Request;
using Framework.Services;
using System.Threading.Tasks;

namespace Demo.Core.Services
{
    public interface ICharacterServices
    {
        Task<ServicesResult> CreateAsync(DragonBallPostRequest model);
        Task<ServicesResult> GetByIDAsync(int characterId);
        Task<ServicesResult> CreateRelative(int id, DragonBallPostRelativeRequest request);
    }
}
