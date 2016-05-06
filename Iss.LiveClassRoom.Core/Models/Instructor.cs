using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class Instructor : User
    {
        public virtual ICollection<Course> Courses { get; set; }
        public bool IsAdmin { get; set; }

        public Instructor() : base()
        {
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
                if (Id.Equals(user.Identity.Name))
                {
                    return Permissions.Full;
                }
                else
                {
                    return Permissions.None;
                }
            }
            return Permissions.View;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
