using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class VideoViewModel : BaseViewModel
    {
        public VideoViewModel() : base(null)
        {
        }
        public VideoViewModel(IEntity entity) : base(entity)
        {
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string CourseId { get; set; }

        public Feed Feed { get; set; }

    }
    public static class VideoExtension
    {
        public static Video ToDomainModel(this VideoViewModel model)
        {
            return new Video()
            {
                Id = model.Id,
                Title=model.Title,
                Course = new Course() { Id = model.CourseId }
            };
        }

        public static VideoViewModel ToViewModel(this Video model)
        {
            return new VideoViewModel(model)
            {
                Id = model.Id,
                Title = model.Title,
                Feed = model,
                CourseId = model.Course.Id,
            };
        }
    }
}