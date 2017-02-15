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
        private Object thisLock = new object();

        public CourseService(IUnitOfWork uow) : base(uow)
        {

        }

        public void AssignStudent(Student student, Course course, string byUserId)
        {
            lock (thisLock)
            {
                if (course.CurrentStudentNumber == course.MaxStudentNumber)
                {
                    //EnrollmentRequest request = new EnrollmentRequest();
                    //request.CourseId = course.Id;
                    //request.StudentId = student.Id;
                    //_uow.GetRepository<EnrollmentRequest>().Add(request);
                    //var i = _uow.Save(byUserId).Result;
                    throw new Exception("The class already reached maximum student number.");
                }
                else
                {
                    course.Students.Add(student);
                    course.CurrentStudentNumber++;
                    student.Courses.Add(course);
                    _uow.GetRepository<Course>().Update(course);
                    _uow.GetRepository<Student>().Update(student);
                    var i = _uow.Save(byUserId).Result;
                }
            }

        }

        public void AssignStudentSync(Student student, Course course, string byUserId)
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
                _uow.GetRepository<Course>().Update(course);
                _uow.GetRepository<Student>().Update(student);
                _uow.SaveSync(byUserId);
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
