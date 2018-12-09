using Demo.Core.Data.MongoDB.Entities;
using Framework.Data.MongoDB;

namespace Demo.Core.Data.MongoDB.Repositories
{
    public interface IUserRepository : IMongoRepositoryBase<UserEntity>
    {
        /// <summary>
        /// Obtém um determinado usuário por e-mail
        /// </summary>
        /// <param name="email">E-mail do usuário.</param>
        UserEntity GetByEmail(string email);

        /// <summary>
        /// Obtém um determinado usuário por refreshToken
        /// </summary>
        /// <param name="token">RefreshToken do usuário.</param>
        UserEntity GetByRefreshToken(string token);
    }
}
