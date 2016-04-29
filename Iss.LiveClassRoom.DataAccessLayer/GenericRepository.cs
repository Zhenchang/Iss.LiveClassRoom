using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Iss.LiveClassRoom.Core;
using System.Linq.Expressions;
using Iss.LiveClassRoom.Core.Repositories;
using Iss.LiveClassRoom.Core.Models;

namespace Iss.LiveClassRoom.DataAccessLayer
{
    public class GenericRepository<T> : IRepository<T> where T : class, IEntity
    {
        internal SystemContext db;
        internal DbSet<T> dbSet;

        public GenericRepository(SystemContext db)
        {
            this.db = db;
            this.dbSet = db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            entity.TimeDeletedUtc = DateTime.UtcNow;
            dbSet.Remove(entity);
        }

        public T GetById(string id)
        {
            return dbSet.SingleOrDefault(m => m.Id == id);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            entity.TimeModifiedUtc = DateTime.UtcNow;
            db.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> GetAll()
        {
            return dbSet.AsQueryable();
        }
    }
}
