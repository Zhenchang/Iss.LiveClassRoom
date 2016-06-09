using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iss.LiveClassRoom.Core.Models;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class EnrollRequestViewModel : BaseViewModel
    {
        public CourseViewModel Course { set; get; }
        public UserViewModel Student { set; get; }

        public EnrollRequestViewModel(IEntity entity) : base(entity) {}
    }
}