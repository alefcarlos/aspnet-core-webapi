using Demo.Application.Data.MySql.Entities;
using Demo.Application.Data.MySql.Repositories;
using Demo.Application.GraphQL.Types.Character.Models;
using Demo.Application.GraphQL.Types.Family.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<CharacterModel> CreateAsync(CharacterModel model)
        {
            var result = await _characterRepository.CreateAsync(new CharacterEntity(model), true);
            if (result == null)
                return null;

            return new CharacterModel(result);
        }

        public async Task<List<CharacterModel>> GetAllAsync()
        {
            var characteres = await _characterRepository.ReadAsync();

            return CharacterModel.ParseEntities(characteres.ToArray());
        }

        public async Task<CharacterModel> GetByIDAsync(int characterId)
        {
            var character =  await _characterRepository.ReadAsync(characterId);

            if (character == null)
                return null;

            return new CharacterModel(character);
        }

        public async Task<List<RelativeModel>> GetRelativesAsync(int characterId)
        {
            var relatives = await _familyRepository.GetRelativesAsync(characterId);

            return relatives.Select(x => new RelativeModel(x)).ToList();
        }
    }
}
