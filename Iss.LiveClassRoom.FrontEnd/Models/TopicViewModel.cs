using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.FrontEnd.Models {

    public class TopicViewModel : BaseViewModel{

        public TopicViewModel() : base(null)
        {
        }
        public TopicViewModel(IEntity entity) : base(entity)
        {
        }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string CourseId { get; set; }

        public Feed Feed { get; set; }
    }


    public static class TopicExtension {
        public static Topic ToDomainModel(this TopicViewModel model) {
            return new Topic()
            {
                Id = model.Id,
                Content = model.Content,
                Title = model.Title,
                Course = new Course() { Id = model.Id}
            };
        }

        public static TopicViewModel ToViewModel(this Topic model) {
            return new TopicViewModel(model)
            {
                Id = model.Id,
                Content = model.Content,
                Title = model.Title,
                Feed = model,
                CourseId = model.Course.Id
            };
        }
    }
}
