using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                Comment = new Comment() { Id = model.CommentId }
            };
        }

        public static ReplyViewModel ToViewModel(this Reply model)
        {
            return new ReplyViewModel()
            {
                Id = model.Id,
                CommentId = model.Comment.Id,
            };
        }
    }
}