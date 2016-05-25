using Iss.LiveClassRoom.WebService.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Iss.LiveClassRoom.WebService.ServiceContracts
{   
    [ServiceContract]
    public interface ICourseWService
    {
        [OperationContract]
        ICollection<CourseData> GetCoursesByStudent(string studentId);
    }
}