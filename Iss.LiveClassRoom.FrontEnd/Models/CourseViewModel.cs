using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class CourseViewModel : BaseViewModel
    {

        public CourseViewModel():base(null) { }
        public CourseViewModel(IEntity entity) : base(entity) { }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        public int MaxStudentNumber { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CurrentStudentNumber { get; set; }

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
                MaxStudentNumber = model.MaxStudentNumber,
                CurrentStudentNumber = model.CurrentStudentNumber,
                Instructor = new Instructor() { Id = model.InstructorId }
            };
        }

        public static CourseViewModel ToViewModel(this Course model)
        {
            return new CourseViewModel(model)
            {
                Id = model.Id,
                Name = model.Name,
                CurrentStudentNumber = model.CurrentStudentNumber,
                MaxStudentNumber = model.MaxStudentNumber,
                InstructorId = model.Instructor?.Id,
                Videos = model.Videos.AsQueryable(),
                Quizzes = model.Quizes.AsQueryable(),
                Students = model.Students.AsQueryable(),
                Topics = model.Topics.AsQueryable()
            };
        }
    }

}