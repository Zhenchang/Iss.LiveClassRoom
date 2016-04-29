using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Services
{
    public interface IService<T> : IDisposable where T : class, IEntity
    {
        Task Add(T entity);

        Task Remove(T entity);

        Task Update(T entity);

        Task<T> GetById(string id);

        IQueryable<T> GetAll();
    }
}
