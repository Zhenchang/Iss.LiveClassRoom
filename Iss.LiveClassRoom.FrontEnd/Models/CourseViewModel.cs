using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class CourseViewModel
    {

        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public string InstructorId { get; set; }
    }


    public static class CourseExtension
    {
        public static Course ToDomainModel(this CourseViewModel model)
        {
            return new Course()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static CourseViewModel ToViewModel(this Course model)
        {
            return new CourseViewModel()
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }

}