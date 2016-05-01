using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class QuizViewModel
    {

        [Required]
        public string Id { get; set; }
        [Required]
        public string Question { get; set; }

        [Required]
        public string CourseId { get; set; }

    }


    public static class QuizExtension
    {
        public static Quiz ToDomainModel(this QuizViewModel model)
        {
            return new Quiz()
            {
                Id = model.Id,
                Question = model.Question,
                CourseId = model.CourseId,

            };
        }

        public static QuizViewModel ToViewModel(this Quiz model)
        {
            return new QuizViewModel()
            {
                Id = model.Id,
                Question = model.Question,
                CourseId = model.CourseId,


            };
        }
    }
}

    

