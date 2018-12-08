using System.Collections.Generic;
using System.Linq;

namespace Framework.Data.EntityFramework
{
    public interface IEFRepository<T> where T : EFEntityBase
    {
        /// <summary>
        /// Obtém todos os registros.
        /// </summary>
        /// <returns></returns>
        ICollection<T> Read();

        /// <summary>
        /// Obtém um registro no banco.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        T Read(params object[] keys);

        /// <summary>
        /// Adiciona um registro no banco.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        T Create(T entity, bool save);

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
        void SaveChanges();

        /// <summary>
        /// Permite criar uma query personalizada.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Query();

        void Dispose();
    }
}
