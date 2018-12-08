using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Data.MongoDB
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

        /// <summary>
        /// Insere um lote de registros no banco de dados.
        /// </summary>
        /// <param name="entities">Dados que serão inseridos no banco de dados.</param>
        public void Create(params T[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            Parallel.ForEach(entities, (x) =>
            {
                x.Created = DateTime.UtcNow;
                x.Updated = DateTime.UtcNow;
            });

            _collection.InsertMany(entities);
        }

        /// <summary>
        /// Insere um lote de registros no banco de dados.
        /// </summary>
        /// <param name="entities">Dados que serão inseridos no banco de dados.</param>
        public async Task CreateAsync(params T[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            Parallel.ForEach(entities, (x) =>
            {
                x.Created = DateTime.UtcNow;
                x.Updated = DateTime.UtcNow;
            });

            await _collection.InsertManyAsync(entities);
        }

        /// <summary>
        /// Método que realiza uma consulta no banco de dados aplicando um filtro e executa um update
        /// na entidade resultado da consulta de acordo com a projeção informada no parametro UpdateDefinition.
        /// </summary>
        /// <param name="filter">Filtro da consulta</param>
        /// <param name="update">Projeção dos campos que serão atualizados.</param>
        /// <param name="options">Configurações opcionais.</param>
        /// <returns></returns>
        public void FindOneAndUpdate(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null)
        {
            if (filter == null)
            {
                throw new ArgumentException("null filter");
            }

            if (update == null)
            {
                throw new ArgumentException("null projection");
            }

            _collection.FindOneAndUpdate(filter, update, options);
        }

        /// <summary>
        /// Método que realiza uma consulta no banco de dados aplicando um filtro e executa um update
        /// na entidade resultado da consulta de acordo com a projeção informada no parametro UpdateDefinition.
        /// </summary>
        /// <param name="filter">Filtro da consulta</param>
        /// <param name="update">Projeção dos campos que serão atualizados.</param>
        /// <param name="options">Configurações opcionais.</param>
        /// <returns></returns>
        public async Task FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null)
        {
            if (filter == null)
            {
                throw new ArgumentException("null filter");
            }

            if (update == null)
            {
                throw new ArgumentException("null projection");
            }

            await _collection.FindOneAndUpdateAsync(filter, update, options);
        }

        /// <summary>
        /// Executa um update na entidade de acordo com o filtro nos campos da projeção informada no parametro UpdateDefinition.
        /// </summary>
        /// <param name="filter">Filtro da consulta</param>
        /// <param name="update">Projeção dos campos que serão atualizados.</param>
        /// <param name="options">Configurações opcionais.</param>
        /// <returns></returns>
        public void UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            if (filter == null)
            {
                throw new ArgumentException("null filter");
            }

            if (update == null)
            {
                throw new ArgumentException("null projection");
            }

            _collection.UpdateOne(filter, update);
        }

        /// <summary>
        /// Executa um update na entidade de acordo com o filtro nos campos da projeção informada no parametro UpdateDefinition.
        /// </summary>
        /// <param name="filter">Filtro da consulta</param>
        /// <param name="update">Projeção dos campos que serão atualizados.</param>
        /// <param name="options">Configurações opcionais.</param>
        /// <returns></returns>
        public async Task UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            if (filter == null)
            {
                throw new ArgumentException("null filter");
            }

            if (update == null)
            {
                throw new ArgumentException("null projection");
            }

            await _collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id) da entidade informada.
        /// </summary>
        /// <param name="entity">
        /// Dado que será removido, utilizará o identificador único (Id) da entidade para remover o registro.
        /// </param>
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _collection.DeleteOne(p => p.Id.Equals(entity.Id));
        }

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id) da entidade informada.
        /// </summary>
        /// <param name="entity">
        /// Dado que será removido, utilizará o identificador único (Id) da entidade para remover o registro.
        /// </param>
        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _collection.DeleteOneAsync(p => p.Id.Equals(entity.Id));
        }

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        public void Delete(string id)
        {
            _collection.DeleteOne(p => p.Id.Equals(id));
        }

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(p => p.Id.Equals(id));
        }


        /// <summary>
        /// Busca e retornar os dados de um registro a partir de um identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        /// <returns>Dados do registro encontrado.</returns>
        public T Read(string id)
        {
            var result = _collection.Find(p => p.Id.Equals(id)).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Busca e retornar os dados de um registro a partir de um identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        /// <returns>Dados do registro encontrado.</returns>
        public async Task<T> ReadAsync(string id)
        {

            var result = await (await _collection.FindAsync(p => p.Id.Equals(id))).FirstOrDefaultAsync();


            return result;
        }

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        public List<T> Read(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }


            var result = _collection.Find(predicate).ToList();

            return result;
        }

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        public List<T> Read(FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }


            var result = _collection.Find(filter).ToList();


            return result;
        }

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        public async Task<List<T>> ReadAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var result = await _collection.FindAsync(predicate);

            return await result.ToListAsync();
        }

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        public async Task<List<T>> ReadAsync(FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            var result = await _collection.FindAsync(filter);

            return await result.ToListAsync();
        }

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro de registro encontrado.</returns>
        public T ReadFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var result = _collection.Find(predicate).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro de registro encontrado.</returns>
        public T ReadFirstOrDefault(FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            var result = _collection.Find(filter).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro registro encontrados.</returns>
        public async Task<T> ReadFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var result = await _collection.Find(predicate).FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro de registro encontrado.</returns>
        public async Task<T> ReadFirstOrDefaultAsync(FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            var result = await _collection.FindAsync(filter);

            return await result.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Atualiza os dados de um registro, caso ele não exista será criado.
        /// </summary>
        /// <param name="entity">Dados do registro a ser atualizado.</param>
        /// <returns>Dados do registro atualizado.</returns>
        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var document = Read(entity.Id);

            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            entity.Updated = DateTime.UtcNow;


            _collection.ReplaceOne(_ => _.Id == entity.Id, entity);

            return entity;
        }

        /// <summary>
        /// Atualiza os dados de um registro, caso ele não exista será criado.
        /// </summary>
        /// <param name="entity">Dados do registro a ser atualizado.</param>
        /// <returns>Dados do registro atualizado.</returns>
        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var document = await ReadAsync(entity.Id);

            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            entity.Updated = DateTime.UtcNow;


            await _collection.ReplaceOneAsync(_ => _.Id == entity.Id, entity);

            return entity;
        }

        ///// <summary>
        ///// Executa ping no banco de dados.
        ///// </summary>
        ///// <returns>true caso ping seja bem sucedido.</returns>
        //public async Task<bool> Ping()
        //{
        //    try
        //    {
        //        var command = new JsonCommand<BsonDocument>("{ ping: 1 }");
        //        await _connection.MongoClient.RunCommand(command);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        ///// <summary>
        ///// Lista de registros paginada.
        ///// </summary>
        ///// <param name="page">Página solicitada.</param>
        ///// <param name="pageSize">Número de registros por página.</param>
        ///// <returns>Lista de registros encontrados na página.</returns>
        //public  List<T> Read(int page, int pageSize)
        //{
        //    if (page <= 0)
        //    {
        //        throw new PaginationPageException("page");
        //    }
        //    else if (pageSize <= 0 || pageSize > 1000)
        //    {
        //        throw new PaginationPageSizeException("pageSize");
        //    }

        //    var watch = BeforeOperation();

        //    var result = _collection.Find(_ => true)
        //        .Skip(pageSize * (page - 1))
        //        .Limit(pageSize)
        //        .ToList();

        //    AfterOperation(watch, new QueryModelLog()
        //    {
        //        Query = "FindPagination " + typeof(T).Name,
        //        EntityParameter = new { page, pageSize }
        //    });

        //    return result;
        //}

        ///// <summary>
        ///// Lista de registros paginada.
        ///// </summary>
        ///// <param name="page">Página solicitada.</param>
        ///// <param name="pageSize">Número de registros por página.</param>
        ///// <returns>Lista de registros encontrados na página.</returns>
        //public  async Task<List<T>> ReadAsync(int page, int pageSize)
        //{
        //    if (page <= 0)
        //    {
        //        throw new PaginationPageException("page");
        //    }
        //    else if (pageSize <= 0 || pageSize > 1000)
        //    {
        //        throw new PaginationPageSizeException("pageSize");
        //    }

        //    var watch = BeforeOperation();

        //    var result = await _collection.Find(_ => true)
        //        .Skip(pageSize * (page - 1))
        //        .Limit(pageSize)
        //        .ToListAsync();

        //    AfterOperation(watch, new QueryModelLog()
        //    {
        //        Query = "FindPagination " + typeof(T).Name,
        //        EntityParameter = new { page, pageSize }
        //    });

        //    return result;
        //}

        ///// <summary>
        ///// Lista de registros paginada.
        ///// </summary>
        ///// <param name="page">Página solicitada.</param>
        ///// <param name="pageSize">Número de registros por página.</param>
        ///// <param name="predicate">Filtro da consulta paginada.</param>
        ///// <returns>Lista de registros encontrados na página.</returns>
        //public  List<T> Read(int page, int pageSize, Expression<Func<T, bool>> predicate)
        //{
        //    if (page <= 0)
        //    {
        //        throw new PaginationPageException("page");
        //    }
        //    else if (pageSize <= 0 || pageSize > 1000)
        //    {
        //        throw new PaginationPageSizeException("pageSize");
        //    }
        //    else if (predicate == null)
        //    {
        //        throw new ArgumentNullException();
        //    }

        //    var watch = BeforeOperation();

        //    var result = _collection.Find(predicate)
        //        .Skip(pageSize * (page - 1))
        //        .Limit(pageSize)
        //        .ToList();

        //    AfterOperation(watch, new QueryModelLog()
        //    {
        //        Query = "FindPaginationPredicate " + typeof(T).Name,
        //        EntityParameter = new { page, pageSize, predicate = predicate.ToJson() }
        //    });

        //    return result;
        //}

        ///// <summary>
        ///// Lista de registros paginada.
        ///// </summary>
        ///// <param name="page">Página solicitada.</param>
        ///// <param name="pageSize">Número de registros por página.</param>
        ///// <param name="filter">Filtro da consulta paginada.</param>
        ///// <returns>Lista de registros encontrados na página.</returns>
        //public  List<T> Read(int page, int pageSize, FilterDefinition<T> filter)
        //{
        //    if (page <= 0)
        //    {
        //        throw new PaginationPageException("page");
        //    }
        //    else if (pageSize <= 0 || pageSize > 1000)
        //    {
        //        throw new PaginationPageSizeException("pageSize");
        //    }
        //    else if (filter == null)
        //    {
        //        throw new ArgumentNullException();
        //    }

        //    var watch = BeforeOperation();

        //    List<T> result;

        //    if (filter == null)
        //    {
        //        result = _collection.Find(_ => true)
        //           .Skip(pageSize * (page - 1))
        //           .Limit(pageSize)
        //           .ToList();
        //    }
        //    else
        //    {
        //        result = _collection.Find(filter)
        //            .Skip(pageSize * (page - 1))
        //            .Limit(pageSize)
        //            .ToList();
        //    }

        //    AfterOperation(watch, new QueryModelLog()
        //    {
        //        Query = "FindPaginationFilter " + typeof(T).Name,
        //        EntityParameter = new { page, pageSize, filter = filter.ToJson() }
        //    });

        //    return result;
        //}

        ///// <summary>
        ///// Lista de registros paginada.
        ///// </summary>
        ///// <param name="page">Página solicitada.</param>
        ///// <param name="pageSize">Número de registros por página.</param>
        ///// <param name="predicate">Filtro da consulta paginada.</param>
        ///// <returns>Lista de registros encontrados na página.</returns>
        //public  async Task<List<T>> ReadAsync(int page, int pageSize, Expression<Func<T, bool>> predicate)
        //{
        //    if (page <= 0)
        //    {
        //        throw new PaginationPageException("page");
        //    }
        //    else if (pageSize <= 0 || pageSize > 1000)
        //    {
        //        throw new PaginationPageSizeException("pageSize");
        //    }

        //    List<T> result;

        //    var watch = BeforeOperation();

        //    if (predicate == null)
        //    {
        //        result = await _collection.Find(_ => true)
        //           .Skip(pageSize * (page - 1))
        //           .Limit(pageSize)
        //           .ToListAsync();
        //    }
        //    else
        //    {
        //        result = await _collection.Find(predicate)
        //            .Skip(pageSize * (page - 1))
        //            .Limit(pageSize)
        //            .ToListAsync();
        //    }

        //    AfterOperation(watch, new QueryModelLog()
        //    {
        //        Query = "FindPaginationPredicateAsync " + typeof(T).Name,
        //        EntityParameter = new { page, pageSize, predicate = predicate.ToJson() }
        //    });

        //    return result;
        //}

        ///// <summary>
        ///// Lista de registros paginada.
        ///// </summary>
        ///// <param name="page">Página solicitada.</param>
        ///// <param name="pageSize">Número de registros por página.</param>
        ///// <param name="filter">Filtro da consulta paginada.</param>
        ///// <returns>Lista de registros encontrados na página.</returns>
        //public  async Task<List<T>> ReadAsync(int page, int pageSize, FilterDefinition<T> filter)
        //{
        //    if (page <= 0)
        //    {
        //        throw new PaginationPageException("page");
        //    }
        //    else if (pageSize <= 0 || pageSize > 1000)
        //    {
        //        throw new PaginationPageSizeException("pageSize");
        //    }

        //    var watch = BeforeOperation();

        //    List<T> result;

        //    if (filter == null)
        //    {
        //        result = await _collection.Find(_ => true)
        //           .Skip(pageSize * (page - 1))
        //           .Limit(pageSize)
        //           .ToListAsync();
        //    }
        //    else
        //    {
        //        result = await _collection.Find(filter)
        //            .Skip(pageSize * (page - 1))
        //            .Limit(pageSize)
        //            .ToListAsync();
        //    }

        //    AfterOperation(watch, new QueryModelLog()
        //    {
        //        Query = "FindPaginationFilterAsync " + typeof(T).Name,
        //        EntityParameter = new { page, pageSize, filter = filter.ToJson() }
        //    });

        //    return result;
        //}

    }
}
