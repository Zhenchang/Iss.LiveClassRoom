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

    }


    public static class InstructorExtension
    {
        public static Instructor ToDomainModel(this InstructorViewModel model)
        {
            return new Instructor()
            {
                Id = model.Id,
                
            };
        }

        public static InstructorViewModel ToViewModel(this Instructor model)
        {
            return new InstructorViewModel()
            {
                Id = model.Id,
               
            };
        }

    }
}
