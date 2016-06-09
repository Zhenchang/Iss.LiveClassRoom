using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.ServiceLayer;
using Iss.LiveClassRoom.DataAccessLayer;
using Iss.LiveClassRoom.Core.Models;
using System.ComponentModel;

namespace Iss.LiveClassRoom.WorkFlow.Activities
{

    public sealed class RemoveEnrollmentRequest : CodeActivity
    {
        public InArgument<string> StudentId { get; set; }

        public InArgument<string> CourseId { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string courseId = context.GetValue(CourseId);
            string studentId = context.GetValue(StudentId);
            IService<EnrollmentRequest> service = new Service<EnrollmentRequest>(new UnitOfWork(new SystemContext()));
            EnrollmentRequest request = service.GetAll().SingleOrDefault(n => n.CourseId.Equals(courseId) & n.StudentId.Equals(studentId));
            request.IsDeleted = true;
            service.Update(request, "###").Wait();
        }
    }
}
