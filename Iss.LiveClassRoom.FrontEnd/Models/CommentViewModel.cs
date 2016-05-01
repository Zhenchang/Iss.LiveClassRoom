using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class CommentViewModel
    {
        [Required]
        public string Id { get; set; }

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
                User = new User() {Id= model.Id },
                Text=model.Text,
                Feed=new Feed() { Id=model.Id},
                
            };
        }

        public static CommentViewModel ToViewModel(this Comment model)
        {
            return new CommentViewModel()
            {
                Id = model.Id,
                UserID=model.User.Id,
                Text = model.Text,
                FeedID=model.Feed.Id,

            };
        }

    }
}