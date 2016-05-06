using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{

    public abstract class User : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }

        [Required]
        public virtual string PasswordHash { get; set; }

        [Required]
        public virtual string PhoneNumber { get; set; }

        public User() : base()
        {
        }


        public override Permissions GetPermissions(IPrincipal user)
        {
            return Permissions.Full;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
