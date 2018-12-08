using Demo.Application.Data.MySql.Entities;
using Demo.Application.Data.MySql.Repositories;
using Demo.Application.GraphQL.Types.Character.Models;
using Demo.Application.GraphQL.Types.Family.Models;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Application.Services.GraphQL
{
    public class CharacterGraphServices : ICharacterGraphServices
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IFamilyRepository _familyRepository;

        public CharacterGraphServices(ICharacterRepository characterRepository, IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
            _characterRepository = characterRepository;
        }

        public CharacterModel Create(CharacterModel model)
        {
            var result = _characterRepository.Create(new CharacterEntity(model), true);
            if (result == null)
                return null;

            return new CharacterModel(result);
        }

        public List<CharacterModel> GetAll()
        {
            var characteres = _characterRepository.Read();

            return CharacterModel.ParseEntities(characteres.ToArray());
        }

        public CharacterModel GetByID(int characterId)
        {
            var character = _characterRepository.Read(characterId);

            if (character == null)
                return null;

            return new CharacterModel(character);
        }

        public List<RelativeModel> GetRelatives(int characterId)
        {
            var relatives = _familyRepository.GetRelatives(characterId);

            return relatives.Select(x => new RelativeModel(x)).ToList();
        }
    }
}
