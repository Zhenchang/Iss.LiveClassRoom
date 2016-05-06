using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class Course : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public virtual Instructor Instructor { get; set; }

        public virtual ICollection<Quiz> Quizes { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public Course() : base()
        {
            Quizes = new HashSet<Quiz>();
            Videos = new HashSet<Video>();
            Topics = new HashSet<Topic>();
            Students = new HashSet<Student>();
        }

        public override Permissions GetPermissions(IPrincipal user)
        {
            var permissions = Permissions.None;
            if (user.IsInRole("Admin"))
            {
                permissions |= Permissions.Create | Permissions.Delete | Permissions.Edit | Permissions.Link | Permissions.View | Permissions.List;
            }
            if (user.IsInRole("Instructor"))
            {
                if (Instructor != null && Instructor.Id.Equals(user.Identity.Name))
                {
                    permissions |= Permissions.Full;
                }
            }else if (user.IsInRole("Student"))
            {
                if (Students.Any(x => x.Id.Equals(user.Identity.Name)))
                {
                    permissions |= Permissions.View | Permissions.List;
                }
            }
            return permissions;
        }

        public override string ToString()
        {
            return Name;
        }

        public IQueryable<Quiz> GetUnAnsweredQuizzesFor(Student student)
        {
            return Quizes.AsQueryable().Except(student.Answers.Select(x=>x.QuizOption.Quiz));
        }
    }
}
