using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iss.LiveClassRoom.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModel(IEntity entity) : base(entity)
        {
        }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Is Admin?")]
        public bool IsAdmin { get; set; }

        public bool IsInstructor { get; set; }

        public ICollection<Course> Courses { get; set; }


    }


    public static class InstructorExtension
    {

        public static UserViewModel ToViewModel(this User model) {
            if (model is Instructor) return (model as Instructor).ToViewModel();
            if (model is Student) return (model as Student).ToViewModel();
            return null;
        }


        public static UserViewModel ToViewModel(this Instructor model)
        {
            return new UserViewModel(model)
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                IsAdmin = model.IsAdmin,
                PhoneNumber = model.PhoneNumber,
                Courses = model.Courses,
                IsInstructor = true
            };
        }

        public static UserViewModel ToViewModel(this Student model) {
            return new UserViewModel(model)
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsInstructor = false,
                Courses = model.Courses,
                IsAdmin = false
            };
        }
    }
}
