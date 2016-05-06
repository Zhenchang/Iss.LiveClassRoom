using Iss.LiveClassRoom.Core.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class CommentViewModel : BaseViewModel
    {
        public CommentViewModel(IEntity entity) : base(entity)
        {

        }

        [Required]
        public string UserID { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string FeedID { get; set; }

    }


    public static class CommentExtension
    {
        public static Comment ToDomainModel(this CommentViewModel model)
        {
            return new Comment()
            {
                Id = model.Id,
                Text=model.Text,
            };
        }

        public static CommentViewModel ToViewModel(this Comment model)
        {
            return new CommentViewModel(model)
            {
                Id = model.Id,
                UserID=model.User.Id,
                Text = model.Text,
                FeedID=model.Feed.Id,

            };
        }

    }
}