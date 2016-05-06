using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class Student : User
    {
        public virtual ICollection<StudentAnswer> Answers { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        public Student() : base()
        {
            Answers = new HashSet<StudentAnswer>();
            Courses = new HashSet<Course>();
        }


        public override Permissions GetPermissions(IPrincipal user)
        {
            if (user.IsInRole("Admin"))
            {
                return Permissions.Full;
            }
            if (user.IsInRole("Instructor"))
            {
                return Permissions.View;
            }
            if (user.IsInRole("Student"))
            {
                if (Id == user.Identity.Name)
                {
                    return Permissions.View;
                }
                else
                {
                    return Permissions.None;
                }
            }

            return Permissions.None;
        }


        public override string ToString()
        {
            return this.Name;
        }
    }

}
