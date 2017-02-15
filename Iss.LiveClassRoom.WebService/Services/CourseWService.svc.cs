﻿using Iss.LiveClassRoom.WebService.ServiceContracts;
using System.Collections.Generic;
using Iss.LiveClassRoom.WebService.DataContracts;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.ServiceLayer.Services;
using Iss.LiveClassRoom.DataAccessLayer;
using System;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using Iss.LiveClassRoom.Core.Repositories;

namespace Iss.LiveClassRoom.WebService.Services
{
    public class CourseWService : ICourseWService
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "Student")]
        public void AnswerQuiz(string optionId)
        {
            string email = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
            IUnitOfWork uow = new UnitOfWork(new SystemContext());
            IQuizService quizService = new QuizService(uow);
            IStudentService studentService = new StudentService(uow);
            Quiz quiz = quizService.GetByOption(optionId);
            //quiz.CheckAuthorization(Permissions.PartialEdit);

            var student = studentService.GetAll().SingleOrDefault(n => n.Email.Equals(email));
            if (student is Student)
            {

                var option = quiz.Options.SingleOrDefault(x => x.Id == optionId);
                // The Student Can't Answer Again
                if (option != null && !student.HasAnsweredQuiz(quiz))
                {
                    option.StudentAnswers.Add(new StudentAnswer()
                    {
                        Time = DateTime.UtcNow,
                        Student = student as Student,
                        QuizOption = option
                    });


                    try
                    {
                        quiz.Course.ToString();
                        option.Quiz.ToString();
                        quizService.Update(quiz, student.Id);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                }
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role="Student")]
        public ICollection<CourseData> GetCoursesByStudentEmail()
        {
            string email = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
            IStudentService studentService = new StudentService(new UnitOfWork(new SystemContext()));
            Student student = studentService.GetAll().SingleOrDefault(n => n.Email.Equals(email));
            ICollection<Course> courses = student.Courses;
            ICollection<CourseData> coursesData = new List<CourseData>();
            foreach (Course course in courses)
            {
                coursesData.Add(new CourseData(course.Id, course.Name, course.Instructor.Name));
            }
            return coursesData;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Student")]
        public ICollection<QuizData> GetQuizListByCourseName(string courseName)
        {
            ICourseService service = new CourseService(new UnitOfWork(new SystemContext()));
            Course course = service.GetAll().SingleOrDefault(n => n.Name.Equals(courseName));
            ICollection<QuizData> quizsData = new List<QuizData>();
            foreach (Quiz quiz in course.Quizes)
            {
                QuizData quizData = new QuizData();
                quizData.Id = quiz.Id;
                quizData.Course = quiz.Course.Name;
                quizData.Question = quiz.Question;
                foreach(QuizOption option in quiz.Options)
                {
                    quizData.Options.Add(new QuizData.OptionData(option.Id, option.Text));
                }
                quizsData.Add(quizData);
            }
            return quizsData;
        }

        public ICollection<VideoData> GetVideoListByCourseName(string courseName)
        {
            ICourseService service = new CourseService(new UnitOfWork(new SystemContext()));
            Course course = service.GetAll().SingleOrDefault(n => n.Name.Equals(courseName));
            ICollection<Video> videos = course.Videos.Where(n => n.IsAccept == 1).ToList();
            ICollection<VideoData> videosData = new List<VideoData>();
            foreach (Video video in videos)
            {
                videosData.Add(new VideoData(video.Id, video.Title, video.FileName));
            }
            return videosData;
        }
    }
}