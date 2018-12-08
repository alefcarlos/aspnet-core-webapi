using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Data.EntityFramework
{
    public class EFRepositoryBase<T> : IEFRepository<T> where T : EFEntityBase
    {
        protected readonly DbContext _context = null;

        public EFRepositoryBase(DbContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public T Create(T entity, bool save)
        {
            entity.CreatedDate = DateTime.Now;
            _context.Set<T>().Add(entity);

            if (save)
                SaveChanges();

            return entity;
        }

        public async Task<T> CreateAsync(T entity, bool save)
        {
            entity.CreatedDate = DateTime.Now;
            await _context.Set<T>().AddAsync(entity);

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

        public Task<T> ReadAsync(params object[] keys) => _context.Set<T>().FindAsync(keys);

        public List<T> Read() =>
            Query().AsNoTracking().ToList();

        public Task<List<T>> ReadAsync() => Query().AsNoTracking().ToListAsync();

        public IQueryable<T> Query() =>
            _context.Set<T>().AsNoTracking();

        public void Update(T entity, bool save)
        {
            entity.UpdatedDate = DateTime.Now;
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
