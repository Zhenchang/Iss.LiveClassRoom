﻿using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Services
{
    public interface IQuizService : IService<Quiz>
    {
        Quiz GetByOption(string optionId);
    }
}
