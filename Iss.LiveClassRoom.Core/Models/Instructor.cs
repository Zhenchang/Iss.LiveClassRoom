using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class Instructor : User
    {
        public virtual ICollection<Course> Courses { get; set; }

        public Instructor() : base()
        {
            Courses = new HashSet<Course>();
        }
    }
}
