using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void RollBack();
        Task<int> Save();
        IRepository<T> GetRepository<T>() where T : class, IEntity;
    }
}
