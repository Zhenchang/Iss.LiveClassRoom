using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }

        [Required]
        public virtual Course Course { get; set; }

        public virtual ICollection<QuizOption> Options { get; set; }

        public Quiz() : base()
        {
            Options = new HashSet<QuizOption>();
        }
    }
}
