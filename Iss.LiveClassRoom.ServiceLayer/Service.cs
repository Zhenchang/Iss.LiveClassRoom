using Iss.LiveClassRoom.Core;
using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Repositories;
using Iss.LiveClassRoom.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.ServiceLayer
{
    public class Service<T> : IService<T> where T: class, IEntity
    {

        protected IUnitOfWork _uow;

        public Service(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Add(T entity)
        {
            _uow.GetRepository<T>().Add(entity);
            _uow.Save();
        }

        public IQueryable<T> GetAll()
        {
            return _uow.GetRepository<T>().GetAll();
        }

        public T GetById(string id)
        {
            return _uow.GetRepository<T>().GetById(id);
        }

        public void Remove(T entity)
        {
            _uow.GetRepository<T>().Remove(entity);
            _uow.Save();
        }

        public void Update(T entity)
        {
            _uow.GetRepository<T>().Update(entity);
            _uow.Save();
        }
    }
}
