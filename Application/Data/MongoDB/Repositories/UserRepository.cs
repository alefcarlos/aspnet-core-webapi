using Clinfy.Application.Data.MongoDB.Entities;
using Framework.Data.MongoDB;
using Framework.Data.MongoDB.Repository;
using MongoDB.Driver;

namespace Clinfy.Application.Data.MongoDB.Repositories
{
    public class UserRepository : MongoRepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(MongoDBConnectionWraper connection) : base(connection)
        {
        }
    }
}
