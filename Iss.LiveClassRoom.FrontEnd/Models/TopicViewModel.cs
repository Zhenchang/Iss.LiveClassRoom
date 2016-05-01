using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.FrontEnd.Models {

    public class TopicViewModel {

        [Required]
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string CourseId { get; set; }
    }


    public static class TopicExtension {
        public static Topic ToDomainModel(this TopicViewModel model) {
            return new Topic()
            {
                Id = model.Id,
                Content = model.Content,
                Course = new Course() { Id = model.Id}
            };
        }

        public static TopicViewModel ToViewModel(this Topic model) {
            return new TopicViewModel()
            {
                Id = model.Id,
                Content = model.Id,
                CourseId = model.Course.Id
            };
        }
    }
}
