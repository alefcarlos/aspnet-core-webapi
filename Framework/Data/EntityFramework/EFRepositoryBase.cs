using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Data.EntityFramework
{
    public class EFRepositoryBase<T> : IEFRepository<T> where T : EFEntityBase
    {
        protected readonly DbContext _context = null;

        public EFRepositoryBase(DbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public T Create(T entity, bool save)
        {
            _context.Set<T>().Add(entity);

            if (save)
                SaveChanges();

            return entity;
        }

        public void Delete(bool save, params object[] keys)
        {
            var e = Read(keys);
            _context.Set<T>().Remove(e);

            if (save)
                SaveChanges();
        }

        public T Read(params object[] keys) => _context.Set<T>().Find(keys);

        public ICollection<T> Read() =>
            Query().ToList();


        public IQueryable<T> Query() =>
            _context.Set<T>();

        public void Update(T entity, bool save)
        {
            _context.Entry(entity).State = EntityState.Modified;

            if (save)
                SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
