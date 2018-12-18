using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Framework.Data.MongoDB
{
    public class MongoEntityBase
    {
        /// <summary>
        /// Identificador único do registro no banco de dados.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        /// <summary>
        /// Data de criação do registro.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        /// Data da última atualização do registro.
        /// </summary>
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
