using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public class Video : Feed
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public virtual Course Course { get; set; }
        
        [Column(TypeName ="int")]
        public int IsAccept { get; set; }

        public string Comment { get; set; }

        public override Permissions GetPermissions(IPrincipal user)
        {
            if (user.IsInRole("Instructor") && Course.Instructor.Id == user.Identity.Name)
            {
                return Permissions.Full;
            }
            if (user.IsInRole("Student"))
            {
                if (Course.Students.Any(x => x.Id == user.Identity.Name))
                {
                    return Permissions.View | Permissions.PartialEdit; // Reply
                }
                else
                {
                    return Permissions.None;
                }
            }

            return Permissions.None;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
