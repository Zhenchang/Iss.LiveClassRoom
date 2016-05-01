using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{
    public interface IEntity
    {
        string Id { get; set; }
        bool IsDeleted { get; set; }

        DateTime TimeCreatedUtc { get; set; }
        DateTime? TimeModifiedUtc { get; set; }
        DateTime? TimeDeletedUtc { get; set; }

        [Required]
        string CreatedByUserId { get; set; }
        string ModifiedByUserId { get; set; }
        string DeletedByUserId { get; set; }

        Permissions GetPermissions(IPrincipal user);

        bool HasAccess(IPrincipal user, Permissions neededPermissions);
    }

    [Flags]
    public enum Permissions : int {
        None = 0,           // 00000
        View = 1,           // 00001
        List = 2,           // 00010
        Create = 4,         // 00100
        Edit = 8,           // 01000
        PartialEdit = 16,   // 10000
        Link = 32,
        Delete = 64,
        Full = View | List | Create | Edit | PartialEdit | Link | Delete,
        Public = View | List,
        ModificationOnly = View | List | PartialEdit | Link
    }
}
