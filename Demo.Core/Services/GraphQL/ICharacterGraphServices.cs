using Demo.Core.GraphQL.Types.Character.Models;
using Demo.Core.GraphQL.Types.Family.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Core.Services.GraphQL
{
    public interface ICharacterGraphServices
    {
        Task<CharacterModel> CreateAsync(CharacterModel model);
        Task<CharacterModel> GetByIDAsync(int characterId);
        Task<List<CharacterModel>> GetAllAsync();
        Task<List<RelativeModel>> GetRelativesAsync(int characterId);
    }
}
