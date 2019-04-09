using Demo.Core.Contracts.DragonBall.Request;
using Demo.Core.Data.MySql.Entities;
using Demo.Core.Data.MySql.Repositories;
using Framework.Data.CacheProviders.Redis;
using Framework.Services;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace Demo.Core.Services
{
    public class CharacterServices : BaseServices, ICharacterServices
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IDistributedCache _redisProvider;

        public CharacterServices(ICharacterRepository characterRepository, IFamilyRepository familyRepository, IDistributedCache redisProvider)
        {
            _familyRepository = familyRepository;
            _redisProvider = redisProvider;
            _characterRepository = characterRepository;
        }

        public async Task<ServicesResult> CreateAsync(DragonBallPostRequest request)
        {
            var entity = await _characterRepository.CreateAsync(new CharacterEntity(request), true);
            return Created(entity);
        }

        public async Task<ServicesResult> CreateRelative(int id, DragonBallPostRelativeRequest request)
        {
            var entity = new FamilyEntity(request)
            {
                CharacterID = id
            };

            var result = await _familyRepository.CreateAsync(entity, true);

            return Created(result);
        }

        public async Task<ServicesResult> GetByIDAsync(int characterId)
        {
            var entity = await _redisProvider.GetAsync($"character:{characterId}", TimeSpan.FromSeconds(30), () => _characterRepository.ReadAsync(characterId));
            return Ok(entity);
        }
    }
}
