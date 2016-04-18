using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core
{

    public interface IEntity {
        string Id { get; set; }

        bool IsDeleted { get; set; }

        DateTime TimeCreatedUtc { get; set; }
        DateTime? TimeModifiedUtc { get; set; }
        DateTime? TimeDeletedUtc { get; set; }

        [Required]
        string CreatedByUserId { get; set; }

        string ModifiedByUserId { get; set; }
        string DeletedByUserId { get; set; }

    }

    public abstract class BaseEntity : IEntity {
        [Key, MaxLength(32)]
        public string Id { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime TimeCreatedUtc { get; set; }
        public DateTime? TimeModifiedUtc { get; set; }
        public DateTime? TimeDeletedUtc { get; set; }

        [Required]
        public string CreatedByUserId { get; set; }
        public string ModifiedByUserId { get; set; }
        public string DeletedByUserId { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString("N");
        }
    }

    public abstract class User : IdentityUser, IEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime TimeCreatedUtc { get; set; }
        public DateTime? TimeModifiedUtc { get; set; }
        public DateTime? TimeDeletedUtc { get; set; }

        [Required]
        public string CreatedByUserId { get; set; }
        public string ModifiedByUserId { get; set; }
        public string DeletedByUserId { get; set; }

        public User() : base()
        {
            Id = Guid.NewGuid().ToString("N");
        }

    }

    public class Instructor : User {
        public virtual ICollection<Course> Courses { get; set; }

        public Instructor() : base()
        {
            Courses = new HashSet<Course>();
        }
    }

    public class Student : User {
        public virtual ICollection<StudentAnswer> Answers { get; set; }

        public Student() : base()
        {
            Answers = new HashSet<StudentAnswer>();
        }
    }

    public class Quiz : BaseEntity {
        [Required]
        public string Question { get; set; }

        [Required]
        public virtual Course Course { get; set; }
        
        public virtual ICollection<QuizOption> Options { get; set; }

        public Quiz() : base()
        {
            Options = new HashSet<QuizOption>();
        }
    }

    public class QuizOption : BaseEntity {
        [Required]
        public string Text { get; set; }

        [Required]
        public virtual Quiz Quiz { get; set; }

        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }

        public QuizOption() : base()
        {
            StudentAnswers = new HashSet<StudentAnswer>();
        }

    }

    public class StudentAnswer : BaseEntity {
        [Required]
        public virtual Student Student { get; set; }

        [Required]
        public virtual QuizOption QuizOption { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
    }

    public abstract class Feed : BaseEntity {
        public virtual ICollection<Comment> Comments { get; set; }

        public Feed(): base()
        {
            Comments = new HashSet<Comment>();
        }
    }

    public class Comment : BaseEntity {
        [Required]
        public virtual User User { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public virtual Feed Feed { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public Comment(): base()
        {
            Replies = new HashSet<Reply>();
        }
    }

    public class Reply : Comment {

        [Required]
        public virtual Comment Comment { get; set; }
        
    }

    public class Video : Feed {
        [Required]
        public string Title { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public virtual Course Course { get; set; }
    }

    public class Course : BaseEntity {
        [Required]
        public string Name { get; set; }

        [Required]
        public virtual Instructor Instructor { get; set; }

        public virtual ICollection<Quiz> Quizes { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }

        public Course() : base()
        {
            Quizes = new HashSet<Quiz>();
            Videos = new HashSet<Video>();
            Topics = new HashSet<Topic>();
        }
    }

    public class Topic : Feed {

        [Required]
        public string Content { get; set; }
        [Required]
        public virtual Course Course { get; set; }
    }
}
