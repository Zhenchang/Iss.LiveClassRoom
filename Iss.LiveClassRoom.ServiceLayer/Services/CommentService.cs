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
    public class CommentService : Service<Comment>, ICommentService
    {
        public CommentService(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
