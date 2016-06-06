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

        public async Task AssignStudent(Student student, Course course, string byUserId)
        {
            if (course.CurrentStudentNumber == course.MaxStudentNumber)
            {
                throw new Exception("The class already reached maximum student number.");
            }
            else
            {
                course.Students.Add(student);
                course.CurrentStudentNumber++;
                student.Courses.Add(course);
                _uow.GetRepository<Student>().Update(student);
                await _uow.Save(byUserId);
            }
        }

        public bool IsFull(string id)
        {
            Course course = _uow.GetRepository<Course>().GetById(id).Result;
            return course.CurrentStudentNumber.Equals(course.MaxStudentNumber);
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
