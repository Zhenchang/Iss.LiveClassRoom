using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class Reply : Comment
    {

        [Required]
        public virtual Comment Comment { get; set; }



        public override Permissions GetPermissions(IPrincipal user)
        {
            return Permissions.View;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
