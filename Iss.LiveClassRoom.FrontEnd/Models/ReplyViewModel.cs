using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class ReplyViewModel
    {

        [Required]
        public string Id { get; set; }

        [Required]
        public string CommentId { get; set; }
    }


    public static class ReplyExtension
    {
        public static Reply ToDomainModel(this ReplyViewModel model)
        {
            return new Reply()
            {
                Id = model.Id,
                CommentId=model.CommentId,

            };
        }

        public static ReplyViewModel ToViewModel(this Reply model)
        {
            return new ReplyViewModel()
            {
                Id = model.Id,
                CommentId = model.CommentId,
            };
        }
    }
}