using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class QuizOptionViewModel
    {

        [Required]
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string QuizId { get; set; }
    }


    public static class QuizOptionExtension
    {
        public static QuizOption ToDomainModel(this QuizOptionViewModel model)
        {
            return new QuizOption()
            {
                Id = model.Id,
                Text=model.Text,
                QuizId=model.QuizId,
                
            };
        }

        public static QuizOptionViewModel ToViewModel(this QuizOption model)
        {
            return new QuizOptionViewModel()
            {
                Id = model.Id,
                Text = model.Text,
                QuizId = model.QuizId,
            };
        }
    }
}