using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public virtual ICollection<QuizOptionViewModel> Options { get; set; }

        public QuizViewModel()
        {
            Options = new List<QuizOptionViewModel>();
        }
    }


    public static class QuizExtension
    {
        public static Quiz ToDomainModel(this QuizViewModel model)
        {
            var q = new Quiz()
            {
                Id = model.Id,
                Question = model.Question,
                Course = new Course() { Id = model.Id }
            };
            foreach (var option in model.Options)
            {
                q.Options.Add(option.ToDomainModel());
            }
            return q;
        }

        public static QuizViewModel ToViewModel(this Quiz model)
        {
            var q = new QuizViewModel()
            {
                Id = model.Id,
                Question = model.Question,
                CourseId = model.Course.Id
            };
            foreach (var option in model.Options)
            {
                q.Options.Add(option.ToViewModel());
            }
            return q;
        }
    }
}

    

