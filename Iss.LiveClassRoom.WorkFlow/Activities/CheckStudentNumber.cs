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

namespace Iss.LiveClassRoom.WorkFlow.Activities
{

    public sealed class CheckStudentNumber : CodeActivity
    {
        public InArgument<string> CourseId { get; set; }
        
        public OutArgument<int> CurrentStudentNumber { get; set; }

        public OutArgument<int> MaxStudentNumber { get; set; }

        public OutArgument<string> WorkFlowInstanceId { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string courseId = context.GetValue(CourseId);
            ICourseService service = new CourseService(new UnitOfWork(new SystemContext()));
            Course course = service.GetById(courseId).Result;
            context.SetValue(MaxStudentNumber, course.MaxStudentNumber);
            context.SetValue(CurrentStudentNumber, course.CurrentStudentNumber);
            //context.SetValue(this.WorkFlowInstanceId, context.WorkflowInstanceId);
        }
    }
}
