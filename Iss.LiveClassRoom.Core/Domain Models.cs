using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core
{

    public interface IEntity {
        string Id { get; set; }
    }

    public abstract class BaseEntity : IEntity {
        public string Id { get; set; }
    }

    public abstract class User : BaseEntity 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Instructor : User {
        public virtual ICollection<Course> Courses { get; set; }
    }

    public class Student : User {
        public virtual ICollection<StudentAnswer> Answers { get; set; }
    }

    public class Quiz : BaseEntity {
        public string Question { get; set; }
        public Course Course { get; set; }
        public virtual ICollection<QuizOption> Options { get; set; }
    }

    public class QuizOption : BaseEntity {
        public string Text { get; set; }
        public Quiz Quiz { get; set; }

        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }

    }

    public class StudentAnswer : BaseEntity {
        public Student Student { get; set; }
        public QuizOption QuizOption { get; set; }
        public DateTime Time { get; set; }
    }

    public abstract class Feed : BaseEntity {
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class Comment : BaseEntity {
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        public Feed Feed { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }

    public class Reply : Comment {
        public Comment Comment { get; set; }
    }

    public class Video : Feed {
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string FileName { get; set; }

        public Course Course { get; set; }
    }

    public class Course : BaseEntity {
        public string Name { get; set; }
        public Instructor Instructor { get; set; }

        public virtual ICollection<Quiz> Quizes { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }

    public class Topic : Feed {
        public Course Course { get; set; }
    }
}
