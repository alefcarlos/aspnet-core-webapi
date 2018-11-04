using Framework.Data.MongoDB.Entities;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Framework.Data.MongoDB.Repository
{
    public class MongoRepositoryBase<T> : IMongoRepositoryBase<T> where T : MongoEntityBase
    {
        /// <summary>
        /// Coleção para acesso aos dados do repositório.
        /// </summary>
        protected readonly IMongoCollection<T> _collection;

        /// <summary>
        /// Conexão com o banco.
        /// </summary>
        protected readonly MongoDBConnectionWraper _connection;

        public MongoRepositoryBase(MongoDBConnectionWraper connection)
        {
            _connection = connection;

            var db = connection.MongoClient.GetDatabase(connection.MongoURL.DatabaseName);

            _collection = db.GetCollection<T>(GetCollectionName());
        }

        private string GetCollectionName()
        {
            var name = typeof(T);
            return name.Name.Replace("Entity", "");
        }

        /// <summary>
        /// Insere um registro no banco de dados.
        /// </summary>
        /// <param name="entity">Dado que será inserido no banco de dados.</param>
        /// <returns>O objeto com o identificador único criado na base de dados.</returns>
        public T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.Created = DateTime.UtcNow;
            entity.Updated = DateTime.UtcNow;

            _collection.InsertOne(entity);

            return entity;
        }

        /// <summary>
        /// Insere um registro no banco de dados.
        /// </summary>
        /// <param name="entity">Dado que será inserido no banco de dados.</param>
        /// <returns>O objeto com o identificador único criado na base de dados.</returns>
        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.Created = DateTime.UtcNow;
            entity.Updated = DateTime.UtcNow;

            await _collection.InsertOneAsync(entity);

            return entity;
        }
    }
}
