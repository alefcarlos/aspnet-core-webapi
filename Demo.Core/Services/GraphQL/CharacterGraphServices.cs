using Demo.Core.Data.MySql.Entities;
using Demo.Core.Data.MySql.Repositories;
using Demo.Core.GraphQL.Types.Character.Models;
using Demo.Core.GraphQL.Types.Family.Models;
using Framework.Data.CacheProviders.Redis;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Core.Services.GraphQL
{
    public class CharacterGraphServices : ICharacterGraphServices
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IDistributedCache _redisProvider;

        public CharacterGraphServices(ICharacterRepository characterRepository, IFamilyRepository familyRepository, IDistributedCache cache)
        {
            _familyRepository = familyRepository;
            _characterRepository = characterRepository;
            _redisProvider = cache;
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
            var entity = await _redisProvider.GetAsync($"character:{characterId}", TimeSpan.FromSeconds(30), () => _characterRepository.ReadAsync(characterId));
            if (entity == null)
                return null;

            return new CharacterModel(entity);
        }

        public async Task<List<RelativeModel>> GetRelativesAsync(int characterId)
        {
            var relatives = await _familyRepository.GetRelativesAsync(characterId);

            return relatives.Select(x => new RelativeModel(x)).ToList();
        }
    }
}
