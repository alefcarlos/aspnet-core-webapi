using Clinfy.Application.Data.MongoDB.Entities;
using Framework.Data.MongoDB;

namespace Clinfy.Application.Data.MongoDB.Repositories
{
    public class UserRepository : MongoRepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(MongoDBConnectionWraper connection) : base(connection)
        {
        }

        /// <summary>
        /// Obtém um determinado usuário por e-mail
        /// </summary>
        /// <param name="email"></param>
        public UserEntity GetByEmail(string email) => ReadFirstOrDefault(x => x.Email.Equals(email));

        /// <summary>
        /// Obtém um determinado usuário por refreshToken
        /// </summary>
        /// <param name="token">RefreshToken do usuário.</param>
        public UserEntity GetByRefreshToken(string token) => ReadFirstOrDefault(x => x.RefreshToken.Equals(token));
    }
}
