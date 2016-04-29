using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class Student : User
    {
        public virtual ICollection<StudentAnswer> Answers { get; set; }

        public Student() : base()
        {
            Answers = new HashSet<StudentAnswer>();
        }
    }

}
