﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class StudentAnswerViewModel
    {
   

        [Required]
        public string Id { get; set; }

        [Required]
        public string StudentId { get; set; }

        [Required]
        public string QuizOptionId { get; set; }
    }


public static class StudentAnswerExtension
    {
    public static StudentAnswer ToDomainModel(this StudentAnswerViewModel model)
    {
        return new StudentAnswer()
        {
            Id = model.Id,
            StudentId=model.StudentId,
            QuizOptionId=model.QuizOptionId,
            
        };
    }

    public static StudentAnswerViewModel ToViewModel(this StudentAnswer model)
    {
        return new StudentAnswerViewModel()
        {
            Id = model.Id,
            StudentId = model.StudentId,
            QuizOptionId = model.QuizOptionId,
        };
    }
}
}