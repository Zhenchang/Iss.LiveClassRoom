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

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
    public static class StudentExtension
    {
        public static Student ToDomainModel(this StudentViewModel model)
        {
            return new Student()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                PasswordHash = model.PasswordHash,
                PhoneNumber = model.PhoneNumber
            };
        }

        public static StudentViewModel ToViewModel(this Student model)
        {
            return new StudentViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                PasswordHash = model.PasswordHash,
                PhoneNumber = model.PhoneNumber
            };
        }
    }
}