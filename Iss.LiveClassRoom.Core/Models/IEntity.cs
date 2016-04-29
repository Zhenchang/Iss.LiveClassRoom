using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

    }
}
