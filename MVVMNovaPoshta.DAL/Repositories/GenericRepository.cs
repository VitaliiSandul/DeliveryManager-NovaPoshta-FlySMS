using MVVMNovaPoshta.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.DAL.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        DbContext context;
        DbSet<T> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void AddOrUpdate(T entity)
        {
            dbSet.AddOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        ~GenericRepository()
        {
            context.Dispose();
        }
    }
}
