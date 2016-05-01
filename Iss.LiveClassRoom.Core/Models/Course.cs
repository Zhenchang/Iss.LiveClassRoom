using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
    }
}
