using Clinfy.Application.Data.MongoDB.Entities;
using Framework.Data.MongoDB.Repository;

namespace Clinfy.Application.Data.MongoDB.Repositories
{
    public interface IUserRepository : IMongoRepositoryBase<UserEntity>
    {
    }
}
