using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class QuizViewModel : BaseViewModel
    {
        [Required]
        public string Question { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{dd-MM-yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{dd-MM-yyyy}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        
        [Display(Name = "Course")]
        public string CourseId { get; set; }

        public virtual ICollection<QuizOptionViewModel> Options { get; set; }

        public QuizViewModel(IEntity entity) : base(entity)
        {
            Options = new List<QuizOptionViewModel>();
        }

        public QuizViewModel() : this(null)
        {
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
                StartDate = model.StartDate,
                EndDate = model.EndDate,
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
            var q = new QuizViewModel(model)
            {
                Id = model.Id,
                Question = model.Question,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
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

    

