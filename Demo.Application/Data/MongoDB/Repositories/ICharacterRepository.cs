using Demo.Application.Data.MongoDB.Entities;
using Framework.Data.MongoDB;

namespace Demo.Application.Data.MongoDB.Repositories
{
    public interface ICharacterRepository : IMongoRepositoryBase<CharacterEntity>
    {
    }
}
