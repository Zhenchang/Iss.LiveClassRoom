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

    public sealed class PushEnrollmentRequest : CodeActivity
    {
        public InArgument<string> CourseId { get; set; }
        public InArgument<string> StudentId { get; set; }
        public OutArgument<int> state { set; get; }
        public OutArgument<string> message { set; get; }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                string courseId = context.GetValue(CourseId);
                string studentId = context.GetValue(StudentId);
                IService<EnrollmentRequest> service = new Service<EnrollmentRequest>(new UnitOfWork(new SystemContext()));
                EnrollmentRequest request = new EnrollmentRequest();
                request.StudentId = studentId;
                request.CourseId = courseId;
                //request.InstanceId = context.WorkflowInstanceId.ToString();
                service.Add(request, studentId).Wait();
                context.SetValue(state, 0);
                context.SetValue(message, "Maximu student limitation. The registration will be reviewed by the instructor.");
            }
            catch (Exception ex)
            {
                context.SetValue(state, 0);
                context.SetValue(message, ex.Message);
            }

        }
    }
}
