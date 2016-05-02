using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class VideoViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string CourseId { get; set; }
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
            return new VideoViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                CourseId = model.Course.Id,
            };
        }
    }
}