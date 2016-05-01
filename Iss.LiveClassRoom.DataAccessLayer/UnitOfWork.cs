using Iss.LiveClassRoom.Core;
using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private SystemContext _db;
        private DbContextTransaction _dbTransaction;
        private ICollection<object> _repositories;

        public UnitOfWork(SystemContext db)
        {
            _repositories = new List<object>();
            _db = db;
            if(_db.Database.CurrentTransaction == null) {
                _dbTransaction = _db.Database.BeginTransaction();
            }else {
                _dbTransaction = _db.Database.CurrentTransaction;
            }
            
        }

        public void Commit()
        {
            _dbTransaction.Commit();
            _dbTransaction.Dispose();
            _dbTransaction = _db.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _dbTransaction.Dispose();
            _db.Dispose();
        }

        public void RollBack()
        {
            _dbTransaction.Rollback();
            _dbTransaction.Dispose();
            _dbTransaction = _db.Database.BeginTransaction();
        }

        public Task<int> Save(string userId)
        {
            return _db.SaveChangesAsync(userId);
        }

        public IRepository<T> GetRepository<T>() where T : class, IEntity
        {
            foreach(IRepository<T> r in _repositories)
            {
                if(r.GetType() is GenericRepository<T>) {
                    return r;
                }
            }
            IRepository<T> repo = new GenericRepository<T>(_db);
            _repositories.Add(repo);
            return repo;
        }
    }
}
