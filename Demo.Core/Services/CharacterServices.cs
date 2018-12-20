using Demo.Core.Contracts.DragonBall.Request;
using Demo.Core.Data.MySql.Entities;
using Demo.Core.Data.MySql.Repositories;
using Framework.Data.CacheProviders;
using Framework.Data.CacheProviders.Redis;
using Framework.Services;
using System;
using System.Threading.Tasks;

namespace Demo.Core.Services
{
    public class CharacterServices : BaseServices, ICharacterServices
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IRedisCacheProvider _redisProvider;

        public CharacterServices(ICharacterRepository characterRepository, IFamilyRepository familyRepository, IRedisCacheProvider redisProvider)
        {
            _familyRepository = familyRepository;
            _redisProvider = redisProvider;
            _characterRepository = characterRepository;
        }

        public async Task<ServicesResult> CreateAsync(DragonBallPostRequest request)
        {
            var result = await _characterRepository.CreateAsync(new CharacterEntity(request), true);
            return Created();
        }

        public async Task<ServicesResult> CreateRelative(int id, DragonBallPostRelativeRequest request)
        {
            var entity = new FamilyEntity(request)
            {
                CharacterID = id
            };

            var result = await _familyRepository.CreateAsync(entity, true);

            return Created();
        }

        public async Task<ServicesResult> GetByIDAsync(int characterId)
        {
            var entity = await _redisProvider.GetAsync($"character:{characterId}", TimeSpan.FromSeconds(30), () => _characterRepository.ReadAsync(characterId));
            return Ok(entity);
        }
    }
}
