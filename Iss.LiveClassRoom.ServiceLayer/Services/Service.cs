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

        public async Task Add(T entity, string byUserId)
        {
            _uow.GetRepository<T>().Add(entity);
            await _uow.Save(byUserId);
            _uow.Commit();

        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        public IQueryable<T> GetAll()
        {
            return _uow.GetRepository<T>().GetAll();
        }

        public Task<T> GetById(string id)
        {
            return _uow.GetRepository<T>().GetById(id);
        }

        public async Task Remove(T entity, string byUserId)
        {
            _uow.GetRepository<T>().Remove(entity);
            await _uow.Save(byUserId);
            _uow.Commit();
        }

        public async Task Update(T entity, string byUserId)
        {
            _uow.GetRepository<T>().Update(entity);
            await _uow.Save(byUserId);
            _uow.Commit();
        }
    }
}
