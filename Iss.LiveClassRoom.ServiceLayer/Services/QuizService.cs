using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iss.LiveClassRoom.Core;
using Iss.LiveClassRoom.Core.Repositories;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.Core.Models;

namespace Iss.LiveClassRoom.ServiceLayer.Services
{
    public class QuizService : Service<Quiz>, IQuizService
    {
        public QuizService(IUnitOfWork uow) : base(uow)
        {

        }


    }
}
