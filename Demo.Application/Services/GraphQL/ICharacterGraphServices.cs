using Demo.Application.GraphQL.Types.Character.Models;
using Demo.Application.GraphQL.Types.Family.Models;
using System.Collections.Generic;

namespace Demo.Application.Services
{
    public interface ICharacterGraphServices
    {
        CharacterModel Create(CharacterModel model);
        CharacterModel GetByID(int characterId);
        List<CharacterModel> GetAll();
        List<RelativeModel> GetRelatives(int characterId);
    }
}
