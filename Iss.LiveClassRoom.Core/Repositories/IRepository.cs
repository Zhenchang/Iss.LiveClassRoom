using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Add(T entity);

        void Remove(T entity);

        void Update(T entity);

        Task<T> GetById(string id);

        IQueryable<T> GetAll();
    }
}
