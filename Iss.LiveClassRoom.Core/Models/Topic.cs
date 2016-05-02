using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class Topic : Feed
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public virtual Course Course { get; set; }
    }
}
