using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iss.LiveClassRoom.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class InstructorViewModel
    {
         
        [Required]
        public string Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsAdmin { get; set; }


    }


    public static class InstructorExtension
    {
        public static Instructor ToDomainModel(this InstructorViewModel model)
        {
            return new Instructor()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                PhoneNumber =  model.PhoneNumber,
                IsAdmin = model.IsAdmin
            };
        }

        public static InstructorViewModel ToViewModel(this Instructor model)
        {
            return new InstructorViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                IsAdmin = model.IsAdmin,
                PhoneNumber = model.PhoneNumber
            };
        }

    }
}
