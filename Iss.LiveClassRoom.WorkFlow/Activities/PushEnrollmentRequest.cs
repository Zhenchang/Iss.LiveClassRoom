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

        protected override void Execute(CodeActivityContext context)
        {
            string courseId = context.GetValue(CourseId);
            string studentId = context.GetValue(StudentId);
            //WorkflowDataContext dataContext = context.DataContext;
            //PropertyDescriptorCollection propertyDescriptorCollection = dataContext.GetProperties();
            //foreach (PropertyDescriptor propertyDesc in propertyDescriptorCollection)
            //{
            //    if (propertyDesc.Name == "CourseId")
            //    {
            //        courseId = propertyDesc.GetValue(dataContext).ToString();
            //        //propertyDesc.SetValue(dataContext, newData);
            //    }
            //    else if (propertyDesc.Name == "StudentId")
            //    {
            //        studentId = propertyDesc.GetValue(dataContext).ToString();
            //    }
            //}
            IService<EnrollmentRequest> service = new Service<EnrollmentRequest>(new UnitOfWork(new SystemContext()));
            EnrollmentRequest request = new EnrollmentRequest();
            request.StudentId = studentId;
            request.CourseId = courseId;
            //request.InstanceId = context.WorkflowInstanceId.ToString();
            service.Add(request, studentId).Wait();
        }
    }
}
