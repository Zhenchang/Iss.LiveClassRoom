using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Services
{
    public interface IService<T> where T : class, IEntity
    {
        void Add(T entity);

        void Remove(T entity);

        void Update(T entity);

        T GetById(string id);

        IQueryable<T> GetAll();
    }
}
