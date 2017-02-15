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

        public Quiz GetByOption(string optionId)
        {
            QuizOption option = _uow.GetRepository<QuizOption>().GetById(optionId).Result;
            IQueryable<Quiz> quizs = _uow.GetRepository<Quiz>().GetAll();
            Quiz quiz = quizs.SingleOrDefault(n => n.Options.Any(m => m.Id.Equals(optionId)));
            return quiz;
        }
    }
}
