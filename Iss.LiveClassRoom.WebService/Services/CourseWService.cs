using Iss.LiveClassRoom.WebService.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iss.LiveClassRoom.WebService.DataContracts;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.Core.Models;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.WebService.Services
{
    public class CourseWService : ICourseWService
    {
        private IStudentService _studentService;

        public CourseWService(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public ICollection<CourseData> GetCoursesByStudent(string studentId)
        {
            Student student = _studentService.GetById(studentId).Result;
            ICollection<Course> courses = student.Courses;
            ICollection<CourseData> coursesData = new List<CourseData>();
            foreach (Course course in courses)
            {
                coursesData.Add(new CourseData(course.Id, course.Name, course.Instructor.Name));
            }
            return coursesData;
        }
    }
}