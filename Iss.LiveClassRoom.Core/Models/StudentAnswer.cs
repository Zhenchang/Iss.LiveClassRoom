using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class StudentAnswer : BaseEntity
    {
        [Required]
        public virtual Student Student { get; set; }

        [Required]
        public virtual QuizOption QuizOption { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }


        public override Permissions GetPermissions(IPrincipal user)
        {
            if (user.IsInRole("Instructor") && QuizOption.Quiz.Course.Instructor.Id == user.Identity.Name)
            {
                return Permissions.View;
            }
            if (user.IsInRole("Student"))
            {
                if (Student.Id == user.Identity.Name)
                {
                    return Permissions.View | Permissions.PartialEdit;
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
            return QuizOption.Text;
        }
    }
}
