using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Data.MongoDB
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

        /// <summary>
        /// Insere um lote de registros no banco de dados.
        /// </summary>
        /// <param name="entities">Dados que serão inseridos no banco de dados.</param>
        void Create(params T[] entities);

        /// <summary>
        /// Insere um lote de registros no banco de dados.
        /// </summary>
        /// <param name="entities">Dados que serão inseridos no banco de dados.</param>
        Task CreateAsync(params T[] entities);

        /// <summary>
        /// Método que realiza uma consulta no banco de dados aplicando um filtro e executa um update
        /// na entidade resultado da consulta de acordo com a projeção informada no parametro UpdateDefinition.
        /// </summary>
        /// <param name="filter">Filtro da consulta</param>
        /// <param name="update">Projeção dos campos que serão atualizados.</param>
        /// <param name="options">Configurações opcionais.</param>
        /// <returns></returns>
        void FindOneAndUpdate(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null);

        /// <summary>
        /// Método que realiza uma consulta no banco de dados aplicando um filtro e executa um update
        /// na entidade resultado da consulta de acordo com a projeção informada no parametro UpdateDefinition.
        /// </summary>
        /// <param name="filter">Filtro da consulta</param>
        /// <param name="update">Projeção dos campos que serão atualizados.</param>
        /// <param name="options">Configurações opcionais.</param>
        /// <returns></returns>
        Task FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null);

        /// <summary>
        /// Executa um update na entidade de acordo com o filtro nos campos da projeção informada no parametro UpdateDefinition.
        /// </summary>
        /// <param name="filter">Filtro da consulta</param>
        /// <param name="update">Projeção dos campos que serão atualizados.</param>
        /// <param name="options">Configurações opcionais.</param>
        /// <returns></returns>
        void UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update);

        /// <summary>
        /// Executa um update na entidade de acordo com o filtro nos campos da projeção informada no parametro UpdateDefinition.
        /// </summary>
        /// <param name="filter">Filtro da consulta</param>
        /// <param name="update">Projeção dos campos que serão atualizados.</param>
        /// <param name="options">Configurações opcionais.</param>
        /// <returns></returns>
        Task UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update);

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id) da entidade informada.
        /// </summary>
        /// <param name="entity">
        /// Dado que será removido, utilizará o identificador único (Id) da entidade para remover o registro.
        /// </param>
        void Delete(T entity);

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id) da entidade informada.
        /// </summary>
        /// <param name="entity">
        /// Dado que será removido, utilizará o identificador único (Id) da entidade para remover o registro.
        /// </param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        void Delete(string id);

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        Task DeleteAsync(string id);

        /// <summary>
        /// Busca e retornar os dados de um registro a partir de um identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        /// <returns>Dados do registro encontrado.</returns>
        T Read(string id);

        /// <summary>
        /// Busca e retornar os dados de um registro a partir de um identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        /// <returns>Dados do registro encontrado.</returns>
        Task<T> ReadAsync(string id);

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        List<T> Read(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        List<T> Read(FilterDefinition<T> filter);

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        Task<List<T>> ReadAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        Task<List<T>> ReadAsync(FilterDefinition<T> filter);

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro de registro encontrado.</returns>
        T ReadFirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro de registro encontrado.</returns>
        T ReadFirstOrDefault(FilterDefinition<T> filter);

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro registro encontrados.</returns>
        Task<T> ReadFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro de registro encontrado.</returns>
        Task<T> ReadFirstOrDefaultAsync(FilterDefinition<T> filter);

        /// <summary>
        /// Atualiza os dados de um registro, caso ele não exista será criado.
        /// </summary>
        /// <param name="entity">Dados do registro a ser atualizado.</param>
        /// <returns>Dados do registro atualizado.</returns>
        T Update(T entity);

        /// <summary>
        /// Atualiza os dados de um registro, caso ele não exista será criado.
        /// </summary>
        /// <param name="entity">Dados do registro a ser atualizado.</param>
        /// <returns>Dados do registro atualizado.</returns>
        Task<T> UpdateAsync(T entity);
    }
}
