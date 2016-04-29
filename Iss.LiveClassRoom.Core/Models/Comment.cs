using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        public virtual User User { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public virtual Feed Feed { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public Comment() : base()
        {
            Replies = new HashSet<Reply>();
        }
    }
}
