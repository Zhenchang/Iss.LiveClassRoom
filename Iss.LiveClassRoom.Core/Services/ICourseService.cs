using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Services
{
    public interface ICourseService : IService<Course>
    {
        Task AssignStudent(Student student, Course course, string byUserId);
        void RemoveStudent(Student student, Course course, string byUserId);

        bool IsFull(string id);
    }
}
