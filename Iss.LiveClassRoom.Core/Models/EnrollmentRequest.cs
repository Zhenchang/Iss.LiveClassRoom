using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class EnrollmentRequest : BaseEntity
    {   
        public string InstanceId { set; get; }

        public string StudentId { set; get; }

        public string CourseId { set; get; }
    }
}
