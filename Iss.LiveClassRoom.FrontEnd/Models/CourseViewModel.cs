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

        public IQueryable<Student> Students { get; set; }
        public IQueryable<Topic> Topics { get; set; }
        public IQueryable<Quiz> Quizzes { get; set; }
        public IQueryable<Video> Videos { get; set; }
    }


    public static class CourseExtension
    {
        public static Course ToDomainModel(this CourseViewModel model)
        {
            return new Course()
            {
                Id = model.Id,
                Name = model.Name,
                Instructor = new Instructor() { Id = model.InstructorId }
            };
        }

        public static CourseViewModel ToViewModel(this Course model)
        {
            return new CourseViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                InstructorId = model.Instructor?.Id,
                Videos = model.Videos.AsQueryable(),
                Quizzes = model.Quizes.AsQueryable(),
                Students = model.Students.AsQueryable(),
                Topics = model.Topics.AsQueryable()
            };
        }
    }

}