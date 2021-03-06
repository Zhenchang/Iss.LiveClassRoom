﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iss.LiveClassRoom.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class QuizOptionViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        public string QuizId { get; set; }

        public int AnswersCount { get; set; }
    }


    public static class QuizOptionExtension
    {
        public static QuizOption ToDomainModel(this QuizOptionViewModel model)
        {
            return new QuizOption()
            {
                Id = model.Id,
                Text=model.Text,
                Quiz = new Quiz() { Id = model.Id}
                
            };
        }

        public static QuizOptionViewModel ToViewModel(this QuizOption model)
        {
            return new QuizOptionViewModel()
            {
                Id = model.Id,
                Text = model.Text,
                QuizId = model.Id,
                AnswersCount = model.StudentAnswers.Count
            };
        }
    }
}