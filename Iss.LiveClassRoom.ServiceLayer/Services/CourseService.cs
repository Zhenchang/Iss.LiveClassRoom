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
    public class CourseService : Service<Course>, ICourseService
    {
        public CourseService(IUnitOfWork uow) : base(uow)
        {

        }

        public void AssignStudent(Student student, Course course, string byUserId)
        {
            course.Students.Add(student);
            student.Courses.Add(course);
            _uow.GetRepository<Student>().Update(student);
            _uow.Save(byUserId);
        }

        public void RemoveStudent(Student student, Course course, string byUserId)
        {
            student.Courses.Remove(course);
            course.Students.Remove(student);
            _uow.GetRepository<Student>().Update(student);
            _uow.Save(byUserId);
        }
    }
}
