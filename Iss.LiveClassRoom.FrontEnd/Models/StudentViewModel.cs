using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class StudentViewModel
    {
        [Required]
        public string Id { get; set; }
    }
    public static class StudentExtension
    {
        public static Student ToDomainModel(this StudentViewModel model)
        {
            return new Student()
            {
                Id = model.Id,
            };
        }

        public static StudentViewModel ToViewModel(this Student model)
        {
            return new StudentViewModel()
            {
                Id = model.Id,
            };
        }
    }
}