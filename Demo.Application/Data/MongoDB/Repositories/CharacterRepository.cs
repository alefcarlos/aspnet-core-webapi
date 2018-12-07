using Demo.Application.Data.MongoDB.Entities;
using Framework.Data.MongoDB;

namespace Demo.Application.Data.MongoDB.Repositories
{
    public class CharacterRepository : MongoRepositoryBase<CharacterEntity>, ICharacterRepository
    {
        public CharacterRepository(MongoDBConnectionWraper connection) : base(connection)
        {
        }
    }
}
