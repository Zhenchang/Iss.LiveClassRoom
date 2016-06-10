using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.ServiceLayer.Services;
using Iss.LiveClassRoom.DataAccessLayer;
using Iss.LiveClassRoom.Core.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using Iss.LiveClassRoom.Core.Repositories;

namespace Iss.LiveClassRoom.WorkFlow.Activities
{

    public sealed class EnrollStudent : CodeActivity
    {
        public InArgument<string> CourseId { get; set; }

        public InArgument<string> StudentId { get; set; }

        public OutArgument<int> CurrentStudentNumber { get; set; }

        public OutArgument<int> state { get; set; }
        public OutArgument<string> message { get; set; } 

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                string courseId = context.GetValue(CourseId);
                string studentId = context.GetValue(StudentId);
                IUnitOfWork uow = new UnitOfWork(new SystemContext());
                ICourseService service = new CourseService(uow);
                IStudentService studentService = new StudentService(uow);
                Course course = service.GetById(courseId).Result;
                Student student = studentService.GetById(studentId).Result;
                service.AssignStudent(student, course, course.Instructor.Id);
                service.Update(course, course.Instructor.Id).Wait();
                context.SetValue(state, 1);
                context.SetValue(message, "Successful!");
            }
            catch (Exception ex)
            {
                context.SetValue(state, 0);
                context.SetValue(message, ex.Message);
            }

        }
    }
}
