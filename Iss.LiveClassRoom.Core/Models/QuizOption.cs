using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class QuizOption : BaseEntity
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public virtual Quiz Quiz { get; set; }

        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }

        public QuizOption() : base()
        {
            StudentAnswers = new HashSet<StudentAnswer>();
        }

    }
}
