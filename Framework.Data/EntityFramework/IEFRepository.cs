using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Data.EntityFramework
{
    public interface IEFRepository<T> where T : EFEntityBase
    {
        /// <summary>
        /// Obtém todos os registros.
        /// </summary>
        /// <returns></returns>
        List<T> Read();

        /// <summary>
        /// Obtém todos os registros.
        /// </summary>
        /// <returns></returns>
        Task<List<T>> ReadAsync();

        /// <summary>
        /// Obtém um registro no banco.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        T Read(params object[] keys);

        /// <summary>
        /// Obtém um registro no banco.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<T> ReadAsync(params object[] keys);

        /// <summary>
        /// Adiciona um registro no banco.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        T Create(T entity, bool save);

        /// <summary>
        /// Adiciona um registro no banco.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        Task<T> CreateAsync(T entity, bool save);

        /// <summary>
        /// Deleta um registro no banco.
        /// </summary>
        /// <param name="save"></param>
        /// <param name="keys"></param>
        void Delete(bool save, params object[] keys);

        /// <summary>
        /// Atualizar um registro no banco.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        void Update(T entity, bool save);

        /// <summary>
        /// Salvar as operações de fato no banco.
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Salvar as operações de fato no banco.
        /// </summary>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Permite criar uma query personalizada.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Query();

        void Dispose();
    }
}
