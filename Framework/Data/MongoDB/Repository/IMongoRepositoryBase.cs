using Framework.Data.MongoDB.Entities;
using System.Threading.Tasks;

namespace Framework.Data.MongoDB.Repository
{
    public interface IMongoRepositoryBase<T> where T : MongoEntityBase
    {
        /// <summary>
        /// Insere um registro no banco de dados.
        /// </summary>
        /// <param name="entity">Dado que será inserido no banco de dados.</param>
        /// <returns>O objeto com o identificador único criado na base de dados.</returns>
        T Create(T entity);

        /// <summary>
        /// Insere um registro no banco de dados.
        /// </summary>
        /// <param name="entity">Dado que será inserido no banco de dados.</param>
        /// <returns>O objeto com o identificador único criado na base de dados.</returns>
        Task<T> CreateAsync(T entity);
    }
}
