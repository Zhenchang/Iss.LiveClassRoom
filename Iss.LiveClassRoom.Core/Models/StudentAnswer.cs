using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    }
}
