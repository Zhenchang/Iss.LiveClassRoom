using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iss.LiveClassRoom.Core.Repositories;

namespace Iss.LiveClassRoom.ServiceLayer.Services
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
