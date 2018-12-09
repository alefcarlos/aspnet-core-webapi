using Demo.Application.Contracts.DragonBall.Request;
using Demo.Application.Data.MySql.Entities;
using Demo.Application.Data.MySql.Repositories;
using Framework.Services;
using System.Threading.Tasks;

namespace Demo.Application.Services
{
    public class CharacterServices : BaseServices, ICharacterServices
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IFamilyRepository _familyRepository;

        public CharacterServices(ICharacterRepository characterRepository, IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
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

        public async Task<ServicesResult> GetByIDAsync(int characterId) => Ok(await _characterRepository.ReadAsync(characterId));
    }
}
