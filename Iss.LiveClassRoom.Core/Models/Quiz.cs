using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class Quiz : BaseEntity
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public virtual Course Course { get; set; }

        public virtual ICollection<QuizOption> Options { get; set; }

        public Quiz() : base()
        {
            Options = new HashSet<QuizOption>();
        }
    }
}
